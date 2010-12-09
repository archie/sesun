@ECHO OFF
ECHO Launching PADIBook application
ECHO Launching Servers (1/5)...
start Server\bin\Debug\Server.exe 58511
ECHO Launching Server(2/5)...
start Server\bin\Debug\Server.exe 56941
ECHO Launching Server(3/5)...
start Server\bin\Debug\Server.exe 57941
ECHO Launching Server(4/5)...
start Server\bin\Debug\Server.exe 58941
ECHO Launching Server(5/5)...
start Server\bin\Debug\Server.exe 58741
ECHO Servers online, ready to launch clients...
pause
start Client\bin\Debug\Client.exe 58510 tcp://localhost:58511
start Client\bin\Debug\Client.exe 56940 tcp://localhost:56941 
start Client\bin\Debug\Client.exe 57940 tcp://localhost:57941
start Client\bin\Debug\Client.exe 58940 tcp://localhost:58941 
start Client\bin\Debug\Client.exe 58740 tcp://localhost:58741
ECHO All done
