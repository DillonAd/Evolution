pipeline {
    agent any
    
    stages {
        stage('Build') {
            steps {
                checkout scm
                sh 'dotnet build ./Evolution/Evolution.csproj --output ./out'
                stash "${BUILD_NUMBER}"
            }
        }
        stage('Unit Test') {
            steps {
                unstash "${BUILD_NUMBER}"
                sh 'dotnet test ./Evolution.Test.Unit/Evolution.Test.Unit.csproj --filter Category=unit --logger "trx;LogFileName=results\tests_unit.xml"'
                stash "${BUILD_NUMBER}"
            }
        }
        stage('Integration Test') {
            steps {
                unstash "${BUILD_NUMBER}"
                sh 'chmod 700 ./integrationTest.sh'
                sh './integrationTest.sh'
            }
        }
    }
}