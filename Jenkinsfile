pipeline {
    agent any

    environment {
        DOTNET_CLI_HOME = "C:\\Program Files\\dotnet"
        DOCKER_IMAGE_NAME = "myappname" // Replace with your desired image name
        DOCKER_REGISTRY = "docker.io" // Replace with your Docker registry URL if needed
        DOCKER_CREDENTIALS_ID = "dockerHub" // Replace with your Jenkins credentials ID for Docker
    }

    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Build') {
            steps {
                script {
                    // Restoring dependencies
                    bat "dotnet restore"

                    // Building the application
                    bat "dotnet build --configuration Release"
                }
            }
        }

        stage('Test') {
            steps {
                script {
                    // Running tests
                    bat "dotnet test --no-restore --configuration Release"
                }
            }
        }

        stage('Publish') {
            steps {
                script {
                    // Publishing the application
                    bat "dotnet publish --no-restore --configuration Release --output .\\publish"
                }
            }
        }

        stage('Build Docker Image') {
            steps {
                script {
                    // Building the Docker image
                    bat "docker build -t ${DOCKER_IMAGE_NAME} ."
                }
            }
        }

    }

    post {
        success {
            echo 'Build, test, publish, and Docker operations successful!'
        }
        failure {
            echo 'There was a failure in the pipeline.'
        }
    }
}