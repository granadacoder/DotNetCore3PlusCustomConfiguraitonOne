

docker build -f scaffolding\docker\ConsoleApp.docker -t myexamples/mycustomconfigconsoleapp .

REM docker run -i --rm myexamples/mycustomconfigconsoleapp
REM OR
REM docker run --env ASPNETCORE_ENVIRONMENT=Development  -i --rm myexamples/mycustomconfigconsoleapp

REM No "port forwarding" with a console app
REM  -p 55555:52400