
// ---------------------------------------MyService.js
SConnect.RegisterNamespace("MyCompany.MyOnCardApp");

MyCompany.MyOnCardApp.MyService = function(readerName,portNum,serviceName){
	this.marshaller = new SConnect.Marshaller(readerName,portNum,serviceName,0x5865A1, 0x09A7);
}

MyCompany.MyOnCardApp.MyService.prototype = {

	getSerialNumber : function(){
		return this.marshaller.invoke(0, 0xA216, MARSHALLER_TYPE_RET_U1ARRAY);
	},

	GetDirs : function(path){
		return this.marshaller.invoke(1, 0x132D, MARSHALLER_TYPE_IN_STRING, path, MARSHALLER_TYPE_RET_STRINGARRAY);
	},

	CreateDir : function(path){
		return this.marshaller.invoke(1, 0x0861, MARSHALLER_TYPE_IN_STRING, path, MARSHALLER_TYPE_RET_S4);
	},

	PutFile : function(path,file){
		return this.marshaller.invoke(2, 0x0FFE, MARSHALLER_TYPE_IN_STRING, path, MARSHALLER_TYPE_IN_U1ARRAY, file, MARSHALLER_TYPE_RET_S4);
	},

	getTheFile : function(path){
		return this.marshaller.invoke(1, 0x8E1E, MARSHALLER_TYPE_IN_STRING, path, MARSHALLER_TYPE_RET_U1ARRAY);
	},

	dispose : function(){
		this.marshaller.dispose();
	}

};
