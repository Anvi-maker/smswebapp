pipeline {
    agent any

    environment {
        DOTNET_CLI_HOME = "C:\\Program Files\\dotnet"
        DOCKER_IMAGE_NAME = "your-docker-image-name" // Replace with your desired image name
        DOCKER_REGISTRY = "your-docker-registry" // Replace with your Docker registry URL if needed
        DOCKER_CREDENTIALS_ID = "your-docker-credentials-id" // Replace with your Jenkins credentials ID for Docker
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

        stage('Push Docker Image') {
            steps {
                script {
                    // Logging in to Docker registry
                    withCredentials([usernamePassword(credentialsId: DOCKER_CREDENTIALS_ID, usernameVariable: 'DOCKER_USERNAME', passwordVariable: 'DOCKER_PASSWORD')]) {
                        bat "echo %DOCKER_PASSWORD% | docker login ${DOCKER_REGISTRY} -u %DOCKER_USERNAME% --password-stdin"
                    }

                    // Pushing the Docker image to the registry
                    bat "docker push ${DOCKER_IMAGE_NAME}"
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