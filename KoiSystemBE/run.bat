@echo off
REM Script to build and push Docker image to Docker Hub

REM Step 1: Build the Docker image
echo Building Docker image...
docker build -t tungb12ok/koisystem .
IF %ERRORLEVEL% NEQ 0 (
    echo Docker image build failed. Exiting...
    pause
    exit /b %ERRORLEVEL%
)
REM Step 3: Push the Docker image to Docker Hub
echo Pushing Docker image to Docker Hub...
docker push tungb12ok/koisystem
IF %ERRORLEVEL% NEQ 0 (
    echo Docker push failed. Exiting...
    pause
    exit /b %ERRORLEVEL%
)

echo Done!
pause
