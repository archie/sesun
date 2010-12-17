del *.txt
del *.xml
copy temp\* .\
pause
start Server\bin\Debug\PKI.exe
pause
start Server\bin\Debug\Server.exe 58511 tcp://localhost:50000
ECHO Launching Server(2/3)...
start Server\bin\Debug\Server.exe 56941 tcp://localhost:50000
ECHO Launching Server(3/3)...
start Server\bin\Debug\Server.exe 57941 tcp://localhost:50000
start Server\bin\Debug\Server.exe 58941 tcp://localhost:50000
pause
start Client\bin\Debug\Client.exe 58510 tcp://localhost:58511
start Client\bin\Debug\Client.exe 56940 tcp://localhost:56941 
start Client\bin\Debug\Client.exe 57940 tcp://localhost:57941
start Client\bin\Debug\Client.exe 58940 tcp://localhost:58941