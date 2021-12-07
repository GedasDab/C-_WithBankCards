// Machine generated C++ stub file (.cpp) for remote object MyService
// Created on : 12/01/2021 02:23:31

#ifdef WIN32
#include <windows.h>
#endif
#include <winscard.h>
#include "MyService.h"

using namespace std;
using namespace Marshaller;


// Constructors
MyService::MyService(string* uri) : SmartCardMarshaller(NULL, 0, uri, (u4)0x5865A1, (u2)0x09A7, 0) { return; }
MyService::MyService(string* uri, u4 index) : SmartCardMarshaller(NULL, 0, uri, (u4)0x5865A1, (u2)0x09A7, index) { return; }
MyService::MyService(u2 portNumber, string* uri) : SmartCardMarshaller(NULL, portNumber, uri, (u4)0x5865A1, (u2)0x09A7, 0) { return; }
MyService::MyService(u2 portNumber, string* uri, u4 index) : SmartCardMarshaller(NULL, portNumber, uri, (u4)0x5865A1, (u2)0x09A7, index) { return; }
MyService::MyService(string* readerName, string* uri) : SmartCardMarshaller(readerName, 0, uri, (u4)0x5865A1, (u2)0x09A7, 0) { return; }
MyService::MyService(string* readerName, u2 portNumber, string* uri) : SmartCardMarshaller(readerName, portNumber, uri, (u4)0x5865A1, (u2)0x09A7, 0) { return; }
MyService::MyService(SCARDHANDLE cardHandle, string* uri) : SmartCardMarshaller(cardHandle, 0, uri, (u4)0x5865A1, (u2)0x09A7) { return; }
MyService::MyService(SCARDHANDLE cardHandle, u2 portNumber, string* uri) : SmartCardMarshaller(cardHandle, portNumber, uri, (u4)0x5865A1, (u2)0x09A7) { return; }

// Pre-defined methods
std::string* MyService::GetReader(void){return GetReaderName();}

// Exposed methods

u1Array* MyService::getSerialNumber(){
	u1Array* _u1Array = NULL;
	Invoke(0, 0xA216, MARSHALLER_TYPE_RET_U1ARRAY, &_u1Array);
	return _u1Array;
}


StringArray* MyService::GetDirs(string* path){
	StringArray* _StringArray = NULL;
	Invoke(1, 0x132D, MARSHALLER_TYPE_IN_STRING, path, MARSHALLER_TYPE_RET_STRINGARRAY, &_StringArray);
	return _StringArray;
}


s4 MyService::CreateDir(string* path){
	s4 _s4 = 0;
	Invoke(1, 0x0861, MARSHALLER_TYPE_IN_STRING, path, MARSHALLER_TYPE_RET_S4, &_s4);
	return _s4;
}


s4 MyService::PutFile(string* path,u1Array* file){
	s4 _s4 = 0;
	Invoke(2, 0x0FFE, MARSHALLER_TYPE_IN_STRING, path, MARSHALLER_TYPE_IN_U1ARRAY, file, MARSHALLER_TYPE_RET_S4, &_s4);
	return _s4;
}


u1Array* MyService::getTheFile(string* path){
	u1Array* _u1Array = NULL;
	Invoke(1, 0x8E1E, MARSHALLER_TYPE_IN_STRING, path, MARSHALLER_TYPE_RET_U1ARRAY, &_u1Array);
	return _u1Array;
}



