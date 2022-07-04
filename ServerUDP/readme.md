# deploy compose
$> docker-compose -f docker-compose.yml -p gps-project up --build
# test connection
$> nc -u 127.0.0.1 27000
$> {"type":"track", "content": "hola mundo"}
