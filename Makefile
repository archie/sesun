CC=g++
CFLAGS=-c -g -Wall -Isrc/ -DDEBUG
LDFLAGS=
DOCSCONFIG=Doxyfile
SOURCES=main.cpp $(filter-out $(SRC_EXCEPT), $(wildcard src/*.cpp))
OBJECTS=$(SOURCES:.cpp=.o)
EXECUTABLE=cc

.PHONY: docs clean graphs

all: $(SOURCES) $(EXECUTABLE)

$(EXECUTABLE): $(OBJECTS) 
	$(CC) $(LDFLAGS) $(OBJECTS) -o $@

.cpp.o:
	$(CC) $(CFLAGS) $< -o $@

docs:
	mkdir -p docs/
	doxygen $(DOCSCONFIG)

clean:
	@rm -f $(EXECUTABLE)
	@rm -f *.o
	@rm -f src/*.o
	@rm -f *~
	@rm -f src/*~


