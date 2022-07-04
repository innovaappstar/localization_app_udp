# build image with docker
$> docker build -t innovadeveloper/udp_server_university:latest -f ./ServerUDP/Dockerfile .


# deploy compose

$> docker-compose -f docker-compose.yml -p gps-project up --build
$> docker-compose -f docker-compose.yml up --build

# test connection

$> nc -u 127.0.0.1 27000
$> {"type":"track", "content": "hola mundo"}

# HiveMQ

url : http://www.hivemq.com/demos/websocket-client/
