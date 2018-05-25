pipeline {
    agent any
    
    stages {
        stage('Build') {
            steps {
                sh 'dotnet build'
                
            }
        }
        stage('Unit Test') {
            steps {
                sh 'dotnet test --results-directory ./  --filter Category=unit -logger "trx;LogFileName=results\unit_tests.xml"'
            }
        }
        stage('Integration Test') {
            steps {

            }
        }
        stage('Deploy') {
            steps {

            }
        }
    }
}