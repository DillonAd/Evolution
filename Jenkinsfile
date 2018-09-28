pipeline {
    agent any
    
    stages {
        stage('Build') {
            steps {
                script {
                    try {
                        checkout scm
                        sh 'dotnet /opt/sonarscanner-msbuild/SonarScanner.MSBuild.dll begin /k:"evolution"'
                        sh 'dotnet build ./Evolution.sln'
                        stash "${BUILD_NUMBER}"
                    } catch(ex) {
                        throw ex
                    } finally {
                        sh 'dotnet /opt/sonarscanner-msbuild/SonarScanner.MSBuild.dll end'
                    }
                    
                }
            }
        }
        stage('Test - Unit') {
            steps {
                script {
                    try {
                        unstash "${BUILD_NUMBER}"
                        sh "dotnet test ./Evolution.Test.Unit/Evolution.Test.Unit.csproj --logger \"trx;LogFileName=unit_tests.xml\" --no-build --filter \"Category=unit\""
            			// step([$class: 'MSTestPublisher', testResultsFile:"**/unit_tests.xml", failOnError: true, keepLongStdio: true])
                        stash "${BUILD_NUMBER}"
                    } catch(ex) {
                        //sh 'dotnet /opt/sonarscanner-msbuild/SonarScanner.MSBuild.dll end'
                        throw ex
                    }
                }
            }
        }
        stage('Integration Test - Oracle') {
            environment {
                String dbName="evolution"
                String oraUser="appUser"
                String oraPwd="appPassword"
                String oraPort1="6666"
                String oraPort2="6667"
                String src = 'sys/Oradoc_db1@localhost:1521/ORCLCDB.localdomain'
                String command = "'source /home/oracle/.bashrc; sqlplus ${src} as sysdba @/SetupOracle.sql; exit \$?'"
                String dockerCmd = "docker exec evolution bash -c ${command}"
            }
            steps {
                script {
                    try {
                        unstash "${BUILD_NUMBER}"
                        //Startup Docker container for database
                        sh "docker run -d --rm --name ${env.dbName} -p ${env.oraPort1}:1521 -p ${env.oraPort2}:5500 -e ORACLE_SID=${env.oraInstance}	store/oracle/database-enterprise:12.2.0.1"

                        timeout(time: 30, unit: 'MINUTES') {
                            sh 'chmod 700 ./Setup/dockerHealth.sh'
                            sh './Setup/dockerHealth.sh ${dbName}'
                        }

                        //Setup test user
                        sh "docker cp ./Setup/SetupOracle.sql ${dbName}:SetupOracle.sql"
                        retry(5) {
                            script {
                                try
                                {
                                    sleep 120
                                    println "${dockerCmd}"
                                    sh "${dockerCmd}"
                                }
                                catch(ex)
                                {
                                    println ex
                                    sleep 15
                                    throw ex
                                }
                            }
                        }
                        
                        sh "dotnet test ./Evolution.Test.Unit/Evolution.Test.Unit.csproj --filter Category=integration --logger \"trx;LogFileName=tests_integration.xml\""
                    }
                    finally
                    {
                        //Breakdown container
                        sh "docker stop ${env.dbName}"

                        //sh 'dotnet /opt/sonarscanner-msbuild/SonarScanner.MSBuild.dll end'
                    }
                }
            }
        }
    }
}