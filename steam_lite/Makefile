.SUFFIXES: .cpp .o

all:	sample

CC=gcc
LIBS=.

OBJS=sample.o
PROG=sample

CFLAGS=-O2
LDFLAGS=-L$(LIBS) -lsteam_api

all:	$(PROG)

clean:
	-rm -f $(PROG) *.o *.obj *.map *~ *.lis core

$(PROG): $(OBJS)
	$(CC) $(LDFLAGS) -o $(PROG) $(OBJS)

.cpp.o:
	$(CC) $(CFLAGS) -c $*.cpp -o $*.o
