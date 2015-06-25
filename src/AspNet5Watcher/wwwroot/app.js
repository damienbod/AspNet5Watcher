!function(){var a=angular.module("mainApp",["ui.router"]);a.config(["$stateProvider","$urlRouterProvider",function(a,b){b.otherwise("/home/createAlarm"),a.state("home",{"abstract":!0,url:"/home",templateUrl:"/templates/home.html"}).state("createAlarm",{parent:"home",url:"/createAlarm",templateUrl:"/templates/createAlarm.html",controller:"alarmsController"})}])}(),function(){"use strict";var a=angular.module("mainApp"),b=function(){function a(a,b,c){a.Vm=this,this.alarmsService=c,this.log=b,this.log.info("alarmsController called"),this.message="Add an alarm to elasticsearch",this.AlarmType="info",this.Message=""}return a.prototype.CreateNewAlarm=function(){console.log("CreateNewAlarm");var a={AlarmType:this.AlarmType,Message:this.Message,Id:""};this.alarmsService.AddAlarm(a)},a.prototype.StartWatcher=function(){console.log("StartWatcher event"),this.alarmsService.StartWatcher()},a.prototype.DeleteWatcher=function(){console.log("DeleteWatcher event"),this.alarmsService.DeleteWatcher()},a}();a.controller("alarmsController",["$scope","$log","alarmsService",b])}(),function(){"use strict";function a(a,b,c){b.info("alarmsService called");var d=function(b){var d=c.defer();return console.log("addAlarm started"),console.log(b),a({url:"api/alarms/AddAlarm",method:"POST",data:b}).success(function(a){d.resolve(a)}).error(function(a){d.reject(a)}),d.promise},e=function(b){var d=c.defer();return console.log("StartWatcher begin"),console.log(b),a({url:"api/WatcherEvents/Start",method:"POST",data:""}).success(function(a){d.resolve(a)}).error(function(a){d.reject(a)}),d.promise},f=function(b){var d=c.defer();return console.log("StartWatcher begin"),console.log(b),a({url:"api/WatcherEvents/Delete",method:"POST",data:""}).success(function(a){d.resolve(a)}).error(function(a){d.reject(a)}),d.promise};return{AddAlarm:d,StartWatcher:e,DeleteWatcher:f}}var b=angular.module("mainApp");b.factory("alarmsService",["$http","$log","$q",a])}();