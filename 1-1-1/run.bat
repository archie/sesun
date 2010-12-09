@ECHO OFF
ECHO Launching PADIBook application
ECHO Launching Servers (1/3)...
start Server\bin\Debug\Server.exe 58511
ECHO Launching Server(2/3)...
start Server\bin\Debug\Server.exe 56941
ECHO Launching Server(3/3)...
start Server\bin\Debug\Server.exe 57941
ECHO Servers online, ready to launch clients...
pause
start Client\bin\Debug\Client.exe 58510 tcp://localhost:58511
start Client\bin\Debug\Client.exe 56940 tcp://localhost:56941 
start Client\bin\Debug\Client.exe 57940 tcp://localhost:57941
ECHO All done
