// Machine generated C++ stub file (.h) for remote object MyService
// Created on : 12/01/2021 02:23:31


#ifndef _include_MyService_h
#define _include_MyService_h

#include <string>
#include <MarshallerCfg.h>
#include <Array.h>
#include <PCSC.h>
#include <Marshaller.h>

#ifdef MyService_EXPORTS
#define MyService_API __declspec(dllexport)
#else
#define MyService_API
#endif

using namespace std;
using namespace Marshaller;

class MyService_API MyService : private SmartCardMarshaller {
public:
	// Constructors
	MyService(string* uri);
	MyService(string* uri, u4 index);
	MyService(u2 portNumber, string* uri);
	MyService(u2 portNumber, string* uri, u4 index);
	MyService(string* readerName, string* uri);
	MyService(string* readerName, u2 portNumber, string* uri);
	MyService(SCARDHANDLE cardHandle, string* uri);
	MyService(SCARDHANDLE cardHandle, u2 portNumber, string* uri);

	// Pre-defined methods
	string* GetReader(void);

	// Exposed methods
	u1Array* getSerialNumber();
	StringArray* GetDirs(string* path);
	s4 CreateDir(string* path);
	s4 PutFile(string* path,u1Array* file);
	u1Array* getTheFile(string* path);
};


#endif
