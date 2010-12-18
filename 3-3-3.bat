del *.txt
del *.xml
copy 3-3-3-clean\* .\
TIMEOUT 5
start Server\bin\Debug\PKI.exe
TIMEOUT 7
start Server\bin\Debug\Server.exe 58511 tcp://localhost:50000
start Server\bin\Debug\Server.exe 56941 tcp://localhost:50000
start Server\bin\Debug\Server.exe 57941 tcp://localhost:50000
TIMEOUT 7
start Server\bin\Debug\Server.exe 58941 tcp://localhost:50000
start Server\bin\Debug\Server.exe 58951 tcp://localhost:50000
start Server\bin\Debug\Server.exe 58961 tcp://localhost:50000
TIMEOUT 8
start Server\bin\Debug\Server.exe 58971 tcp://localhost:50000
start Server\bin\Debug\Server.exe 58981 tcp://localhost:50000
start Server\bin\Debug\Server.exe 58991 tcp://localhost:50000
TIMEOUT 9
start Client\bin\Debug\Client.exe 58510 tcp://localhost:58511
start Client\bin\Debug\Client.exe 56940 tcp://localhost:56941 
start Client\bin\Debug\Client.exe 57940 tcp://localhost:57941
TIMEOUT 10
start Client\bin\Debug\Client.exe 58940 tcp://localhost:58941
start Client\bin\Debug\Client.exe 58950 tcp://localhost:58951
start Client\bin\Debug\Client.exe 58960 tcp://localhost:58961
TIMEOUT 11
start Client\bin\Debug\Client.exe 58970 tcp://localhost:58971
start Client\bin\Debug\Client.exe 58980 tcp://localhost:58981
start Client\bin\Debug\Client.exe 58990 tcp://localhost:58991