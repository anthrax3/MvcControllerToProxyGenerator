# MvcControllerToProxyGenerator
Creates JavaScript or TypeScript AJAX proxies for Angular 5.x, AngularJs 1.x or jQuery for .NET Controller or WebApi functions.

Download and install the [NuGet package for "TypeScriptAngularJsProxyGenerator"](https://www.nuget.org/packages/TypeScriptAngularJsProxyGenerator/) into your WebProject.

Or install the NuGet package with the package manager console:

**`PM > Install-Package TypeScriptAngularJsProxyGenerator`**

Be carefull on updating the package, save your custom settings first, because updating this package will replace your settings for ProxyGenerator or use the **web.config** to store your Settings.

For questions or problems please open a GitHub Issue!

Improvements: Now you can also Load 64Bit DLLs with T4.

Now Supports HttpClient for Angular 5.x

---------

## Setup / Usage
The NuGet package installs a T4 Template **`Scripts\ProxyGeneratorScript.tt`** in your WebProject. 

1. Configure the T4 template settings
2. Configure your controllers by adding the right CreateProxy attribute to each function (AJAX call)
3. Build the entire Solution, because the T4 Script looks up your compiled assemblies for the added attributes
4. Run the T4 Script with "**Run Custom Tool**"

For detailed infos, please read the complete Readme :-)


## The NuGet Package "TypeScriptAngularJsProxyGenerator"
The package installs a T4 Template into your WebProject under the path

`Scripts\ProxyGeneratorScript.tt`

and adds a refrence to a installed DLL named

`ProxyGenerator.dll`

which is set in the T4 template as required reference.

In a earlier version of this NuGet package I've installed also the depended [NuGet Package "Microsoft.VisualStudio.TextTemplating.14.0"](https://www.nuget.org/packages/Microsoft.VisualStudio.TextTemplating.14.0/) but then you can't use the ProxyGenerator in .NET 4.0 projects.
So I've removed this package, if you got some excecute problems, that dependency "Microsoft.VisualStudio.TextTemplating" was not found, you need to Install this package manual.

If you want to **create TypeScript proxies** then, you **need to install manually** the [NuGet Package for TypeLite](https://www.nuget.org/packages/TypeLite/)

(If you use TypeScript don't forget to install the TypeDefinitions for jQuery and/or AngularJs)

## T4 Configruation Settings
When you have installed all NuGet Packages, you **need to configure** the T4 Template install location:

`Scripts\ProxyGeneratorScript.tt` 

in the config section **"SETTINGS for MANUAL adjustments"**, here you can find some settings you can/need to change, that the generator works right.

Here you **NEED to set** the name of your current WebPoject. Warning: If you have manually renamed the web directory where your website is located, then you need to insert the foldername here and NOT the WebprojectName.

`settings.WebProjectName = "ProxyGeneratorDemoPage";`

If you want to set the output directory for your created proxy files, then you set a path from the root of your WebProject. If you want to create the files directly below the T4 template set this setting to null or empty string.

`settings.ProxyFileOutputPath =  @"ScriptsApp\Services\";`

If you want to create a TypeScript Proxy, don't forget to install [TypeLite](https://www.nuget.org/packages/TypeLite/) and you need to add to the TypeLite T4 template the following line:

`.WithFormatter((type, f) => "I" + ((TypeLite.TsModels.TsClass)type).Name)`

or if you use the original TypeLite Interface name without the "I" then, you need to remove the "I" from my T4 Template.
     
`settings.TypeLiteInterfacePrefix = "";`

### Alternative: Store the proxysettings in the Web.config
You can also store the settings in the **web.config**, then its not possible that you overwrite your proxysettings, when you upgrade the ProxyGenerator via NuGet.
You can only add the settings you need to your web.cofig, the remaining settings will be loaded from the T4 Template, the proxysettings in the web.config will overwrite the T4 proxysettings:

    <appSettings>
       <!-- Proxy Generator Settings - START -->
       <add key="ProxyGenerator_WebProjectName" value="ProxyGeneratorDemoPage" />
       <add key="ProxyGenerator_ProxyFileOutputPath" value="ScriptsApp\Services\" />
       <add key="ProxyGenerator_LowerFirstCharInFunctionName" value="true" />
       <add key="ProxyGenerator_TypeLiteInterfacePrefix" value="I" />
	   <add key="ProxyGenerator_ServicePrefixUrl" value="" />
       <!-- Tell the ProxyGenerator which suffix the generated controllername will have -->
       <add key="ProxyGenerator_TemplateSuffix_AngularJs" value="AngularJsSrv" />
       <add key="ProxyGenerator_TemplateSuffix_AngularTs" value="PService" />
       <add key="ProxyGenerator_TemplateSuffix_Angular2Ts" value=".service" />
       <add key="ProxyGenerator_TemplateSuffix_jQueryJs" value="jQueryJs" />
       <add key="ProxyGenerator_TemplateSuffix_jQueryTs" value="jQueryTs" />
	   <!-- Set Different Output Pathes for each ScriptType. If no value is the or the keys are missing in the Web.config the default outputpath "ProxyGenerator_ProxyFileOutputPath" is used. The Outpath Settings can only be set in the web.config! -->
       <add key="ProxyGenerator_OutputPath_jQueryTsModule" value="ScriptsApp\ServicesJQuery\" />
       <add key="ProxyGenerator_OutputPath_jQueryJsModule" value="ScriptsApp\ServicesJQuery\" />
       <add key="ProxyGenerator_OutputPath_AngularJsModule" value="ScriptsApp\Services\" />
       <add key="ProxyGenerator_OutputPath_AngularTsModule" value="ScriptsApp\Services\" />
	   <add key="ProxyGenerator_OutputPath_Angular2TsModule" value="ScriptsAppNg2\Services\" />
       <!-- Proxy Generator Settings - END -->
    </appSettings>

#### "ServicePrefixUrl" in Detail

When you need to add a prefix Url before each Service Call then you need to use this Option and fill in e.g. "api" or "api/test".
For the "api" example the T4 Proxy would create a service call with the "api" before the original address like

		 public loadTsCallByParams(name: string,vorname: string,alter: number) : Observable<ProxyGeneratorDemoPage.Models.Person.Models.IPerson> { 
				return this.http.get('api/Proxy/LoadData').map((response: Response)  => <ProxyGeneratorDemoPage.Models.Person.Models.IPerson>response.json() as ProxyGeneratorDemoPage.Models.Person.Models.IPerson);
		 } 


## How to tell the T4 template to create a proxy 
The T4 template only creates proxies for controller functions which are decorated with the right Attribute.
For each controller, framework and language a new file with the ControllerName (Classname) + Suffix is created (you can change the Suffix in the T4 Template).

The ProxyGenerator DLL provides four different attributes.

| Attribute Name | Language | Framework | required Attribute Parameter | optional Parameter
|----------------|----------|-----------|-----------------|---------|
|CreateAngularJsProxyAttribute| JavaScript | AngularJs | -|CreateWindowLocationHrefLink
|CreateAngularTsProxyAttribute| TypeScript | AngularJs | ReturnType|CreateWindowLocationHrefLink
|CreateAngular2TsProxyAttribute| TypeScript | Angular 2 | ReturnType|CreateWindowLocationHrefLink
|CreateJQueryJsProxyAttribute| JavaScript | jQuery | -|CreateWindowLocationHrefLink
|CreateJQueryTsProxyAttribute| TypeScript | jQuery | ReturnType|CreateWindowLocationHrefLink

You can mix these attributes in any combination. It is possible to use all on the same controller function, then for this function five different proxies are create (one for each language and framework).

**Attribute Parameter:**

- **ReturnType** => is only used in TypeScript proxies and generates proxies with the right ReturnType of your .NET JSON Call in TypeScript (you also need TypeLite T4 Template)
- **CreateWindowLocationHrefLink** => The controller methods with this attribute parameter set to true only generates a "window.location.href" function in the proxy. It can be used to call download links like in the examples further down.

Before you can start the proxy creation, you need to **rebuild your solution**, because the T4 template looks up your compiled assemblies for the added proxy creation attributes.

When the rebuild was successfull, then start the proxy creation by right clicking on the T4 Template `ProxyGeneratorScript.tt` and choose **Run Custom Tool**.

(**Hint:** Take a look at the GitHub code, there you find a solution with the T4 template and also a website with examples for the attribute usage shown below)

### Example: Angular 2 TypeScript Proxy - CreateAngular2TsProxyAttribute
Creates a proxy for Angular 2 in TypeScript.

The .NET controller functions are decorated with the attribute "**CreateAngular2TsProxyAttribute**" and you also need to add the "ReturnType" to the attribute params.
The "ReturnType" is the .NET type of the Json which is returned by the Json Function.

    using ProxyGenerator.ProxyTypeAttributes;

    public class ProxyController : Controller
    {  
        [CreateAngular2TsProxy(ReturnType = typeof(Person))]
        public JsonResult AddTsEntryOnly(Person person)
        {
            return Json(person, JsonRequestBehavior.AllowGet);
        }

        [CreateAngular2TsProxy(ReturnType = typeof(Auto))]
        public JsonResult AddTsEntryAndName(Person person, string name)
        {
            return Json(new Auto() { Marke = name }, JsonRequestBehavior.AllowGet);
        }

        [CreateAngular2TsProxy(ReturnType = typeof(Person))]
        public JsonResult LoadTsCallById(int id)
        {
            return Json(new Person() { Id = id }, JsonRequestBehavior.AllowGet);
        }

        [CreateAngular2TsProxy(ReturnType = typeof(Person))]
        public JsonResult LoadTsCallByParams(string name, string vorname, int alter)
        {
            return Json(new Person() { Name = name, Id = alter }, JsonRequestBehavior.AllowGet);
        }

        [CreateAngular2TsProxy(ReturnType = typeof(Auto))]
        public JsonResult LoadTsCallByParamsWithEnum(string name, string vorname, int alter, ClientAccess access)
        {
            return Json(new Auto() { Marke = name, Alter = alter }, JsonRequestBehavior.AllowGet);
        }

        [CreateAngular2TsProxy(ReturnType = typeof(List<Auto>))]
        public JsonResult LoadAllAutosListe(string name)
        {
            return Json(new List<Auto>() { new Auto() { Marke = name }, new Auto() }, JsonRequestBehavior.AllowGet);
        }

        [CreateAngular2TsProxy(ReturnType = typeof(Person))]
        public JsonResult ClearTsCall()
        {
            return Json(new Person(), JsonRequestBehavior.AllowGet);
        }

        [CreateAngular2TsProxy(ReturnType = typeof(void))]
        public JsonResult VoidTsReturnType(string name)
        {
            return Json(string.Empty, JsonRequestBehavior.AllowGet);
        }

        [CreateAngular2TsProxy(ReturnType = typeof(string))]
        public JsonResult StringTsReturnType(string name)
        {
            return Json(name, JsonRequestBehavior.AllowGet);
        }

        [CreateAngular2TsProxy(ReturnType = typeof(int))]
        public JsonResult IntegerTsReturnType(int age)
        {
            return Json(age, JsonRequestBehavior.AllowGet);
        }

        [CreateAngular2TsProxy(ReturnType = typeof(DateTime))]
        public JsonResult DateTsReturnType(string name)
        {
            return Json(DateTime.Now, JsonRequestBehavior.AllowGet);
        }

        [CreateAngular2TsProxy(ReturnType = typeof(Boolean))]
        public JsonResult BoolTsReturnType(bool boolValue)
        {
            return Json(boolValue, JsonRequestBehavior.AllowGet);
        }

        [CreateAngular2TsProxy(CreateWindowLocationHrefLink = true)]
        public FileResult GetDownloadSimple(int companyId, string name)
        {
            var fileContent = Encoding.ASCII.GetBytes(string.Format("Das ist ein Test Download für die CompanyId: {0} mit dem Namen: {1}", companyId, name));
            return File(fileContent, "text/text", "TestDL.txt");
        }
    }

 this will create the following Angular 2 TypeScript proxy service. With the right "ReturnTypes" for each proxy call, please install TypeLite to create the TypeScript interfaces for each type. 
 
**Hint:** Also TypeLite uses attributes to create the interfaces for your classes, you need to set the "[TsClass]" attribute on the classes which are returned by your Json Calls to create the interfaces with TypeLite T4 template.

    import {Injectable} from '@angular/core';
    import {Http, Response} from '@angular/http';
    import {Observable} from 'rxjs/observable';
    import 'rxjs/add/operator/map';

    @Injectable()
    export class Proxyservice { 

    constructor(private http: Http) {  }

         public addTsEntryOnly(person: ProxyGeneratorNgDemoPage.Models.IPerson) : Observable<ProxyGeneratorNgDemoPage.Models.IPerson> { 
            return this.http.post('Proxy/AddTsEntryOnly',person) as Observable<ProxyGeneratorNgDemoPage.Models.IPerson>;
         } 

         public addTsEntryAndName(person: ProxyGeneratorNgDemoPage.Models.IPerson,name: string) : Observable<ProxyGeneratorNgDemoPage.Models.IAuto> { 
            return this.http.post('Proxy/AddTsEntryAndName'+ '?name='+encodeURIComponent(name),person) as Observable<ProxyGeneratorNgDemoPage.Models.IAuto>;
         } 

		 public loadTsCallById(id: number) : Observable<ProxyGeneratorNgDemoPage.Models.IPerson> { 
            return this.http.get('Proxy/LoadTsCallById' + '/' + id) as Observable<ProxyGeneratorNgDemoPage.Models.IPerson>;
		 } 

		 public loadTsCallByParams(name: string,vorname: string,alter: number) : Observable<ProxyGeneratorNgDemoPage.Models.IPerson> { 
            return this.http.get('Proxy/LoadTsCallByParams'+ '?name='+encodeURIComponent(name)+'&vorname='+encodeURIComponent(vorname)+'&alter='+alter) as Observable<ProxyGeneratorNgDemoPage.Models.IPerson>;
		 } 

		 public loadTsCallByParamsWithEnum(name: string,vorname: string,alter: number,access: ProxyGeneratorNgDemoPage.Models.ClientAccess) : Observable<ProxyGeneratorNgDemoPage.Models.IAuto> { 
		    return this.http.get('Proxy/LoadTsCallByParamsWithEnum'+ '?name='+encodeURIComponent(name)+'&vorname='+encodeURIComponent(vorname)+'&alter='+alter+'&access='+access) as Observable<ProxyGeneratorNgDemoPage.Models.IAuto>;
		 } 

		public loadAllAutosListe(name: string) : Observable<ProxyGeneratorNgDemoPage.Models.IAuto[]> { 
            return this.http.get('Proxy/LoadAllAutosListe'+ '?name='+encodeURIComponent(name)) as Observable<ProxyGeneratorNgDemoPage.Models.IAuto[]>;
		} 

		 public clearTsCall() : Observable<ProxyGeneratorNgDemoPage.Models.IPerson> { 
            return this.http.get('Proxy/ClearTsCall') as Observable<ProxyGeneratorNgDemoPage.Models.IPerson>;
		} 

		public voidTsReturnType(name: string) : void  { 
			this.http.get('Proxy/VoidTsReturnType'+ '?name='+encodeURIComponent(name)).subscribe(res => res); 
	    } 

		public stringTsReturnType(name: string) : Observable<string> { 
            return this.http.get('Proxy/StringTsReturnType'+ '?name='+encodeURIComponent(name)) as Observable<string>;
		} 

		public integerTsReturnType(age: number) : Observable<number> { 
            return this.http.get('Proxy/IntegerTsReturnType'+ '?age='+age) as Observable<number>;
		} 

		public dateTsReturnType(name: string) : Observable<any> { 
            return this.http.get('Proxy/DateTsReturnType'+ '?name='+encodeURIComponent(name)) as Observable<any>;
		} 

		public boolTsReturnType(boolValue: boolean) : Observable<boolean> { 
            return this.http.get('Proxy/BoolTsReturnType'+ '?boolValue='+boolValue) as Observable<boolean>;
		} 

		public errorStringReturnType(boolValue: boolean) : Observable<string> { 
            return this.http.get('Proxy/ErrorStringReturnType'+ '?boolValue='+boolValue) as Observable<string>;
		} 

		public getDownloadSimple(companyId: number,name: string) : void  { 
			window.location.href = 'Proxy/GetDownloadSimple'+ '?companyId='+companyId+'&name='+encodeURIComponent(name); 
		} 
    }


#### Example for Angular 2 TypeScript Proxy - FileUpload
please use **HttpPostedFileBase** Type for fileupload, then the Proxy is created the right way.

    [CreateAngular2TsProxy(ReturnType = typeof(Person))]
    public ActionResult AddFileToServer(HttpPostedFileBase datei, int detailId)
    {
    	//TODO Do something with the uploaded File - YOU NEED TO NAME YOUR "HttpPostedFileBase" property "datei"!!!!!!
    	//because the Proxy generates the formData with the name "datei"!
        return Json(new Person() { Id = detailId }, JsonRequestBehavior.AllowGet);
    }

this will create the following function in TypeScript

     public addFileToServer(datei: any,detailId: number) : Observable<ProxyGeneratorNgDemoPage.Models.IPerson> { 
		   var formData = new FormData(); 
		   formData.append('datei', datei); 
           return this.http.post('Proxy/AddFileToServer'+ '?detailId='+detailId,formData) as Observable<ProxyGeneratorNgDemoPage.Models.IPerson>;
     } 

### Example: AngularJs JavaScript Proxy - CreateAngularJsProxyAttribute
Creates a proxy for AngularJs in JavaScript.

The .NET controller functions are decorated with the attribute "**CreateAngularJsProxyAttribute**"

    using ProxyGenerator.ProxyTypeAttributes;
   
    public class ProxyController : Controller
    {     
        [CreateAngularJsProxy]
        public JsonResult AddJsEntryOnly(Person person)
        { 
            return Json(person, JsonRequestBehavior.AllowGet);
        }

        [CreateAngularJsProxy]
        public JsonResult AddJsEntryAndName(Person person, string name)
        {
            return Json(new Auto() { Marke =  name}, JsonRequestBehavior.AllowGet);
        }

        [CreateAngularJsProxy]
        public JsonResult AddJsEntryAndParams(Person person, string name, string vorname)
        {
            return Json(new Auto() { Marke = name}, JsonRequestBehavior.AllowGet);
        }

        [CreateAngularJsProxy]
        public JsonResult ClearJsCall()
        {
            return Json("ClearJsCall was Called", JsonRequestBehavior.AllowGet);
        }

        [CreateAngularJsProxy]
        public JsonResult LoadJsCallById(int id)
        {
            return Json(id, JsonRequestBehavior.AllowGet);
        }

        [CreateAngularJsProxy]
        public JsonResult LoadJsCallByParams(string name, string vorname, int alter)
        {
            return Json(vorname, JsonRequestBehavior.AllowGet);
        }

        [CreateAngularJsProxy(CreateWindowLocationHrefLink = true)]
        public FileResult GetDownloadSimple(int companyId, string name)
        {
            var fileContent = Encoding.ASCII.GetBytes(string.Format("This is a download for the CompanyId: {0} with the Name: {1}", companyId, name));
            return File(fileContent, "text/text", "TestDL.txt");
        }


this will create the following AngularJs JavaScript proxy directly localted in VS "below" the T4 template.

    //Warning this file was dynamicly created.
    //Please don't change any code it will be overwritten next time the template is executed.
    //Created on 13.01.2016 time 20:48 from SquadWuschel.
    
    function proxyAngularJsSrv($http) { this.http = $http; } 
    
    
    proxyAngularJsSrv.prototype.addJsEntryOnly = function (person) { 
       return this.http.post('Proxy/AddJsEntryOnly',person).then(function (result) {
            return result.data;
       });
    }
    
    proxyAngularJsSrv.prototype.addJsEntryAndName = function (person,name) { 
       return this.http.post('Proxy/AddJsEntryAndName'+ '?name='+encodeURIComponent(name),person).then(function (result) {
            return result.data;
       });
    }
    
    proxyAngularJsSrv.prototype.addJsEntryAndParams = function (person,name,vorname) { 
       return this.http.post('Proxy/AddJsEntryAndParams'+ '?name='+encodeURIComponent(name)+'&vorname='+encodeURIComponent(vorname),person).then(function (result) {
            return result.data;
       });
    }
    
    proxyAngularJsSrv.prototype.clearJsCall = function () { 
       return this.http.get('Proxy/ClearJsCall').then(function (result) {
            return result.data;
       });
    }
    
    proxyAngularJsSrv.prototype.loadJsCallById = function (id) { 
       return this.http.get('Proxy/LoadJsCallById' + '/' + id).then(function (result) {
            return result.data;
       });
    }
    
    proxyAngularJsSrv.prototype.loadJsCallByParams = function (name,vorname,alter) { 
       return this.http.get('Proxy/LoadJsCallByParams'+ '?name='+encodeURIComponent(name)+'&vorname='+encodeURIComponent(vorname)+'&alter='+alter).then(function (result) {
            return result.data;
       });
    }

    proxyAngularJsSrv.prototype.getDownloadSimple = function (companyId,name) { 
        window.location.href = 'Proxy/GetDownloadSimple'+ '?companyId='+companyId+'&name='+encodeURIComponent(name)
    } 
    
    angular.module('proxyAngularJsSrv', []) .service('proxyAngularJsSrv', ['$http', proxyAngularJsSrv]);


#### Example for AngularJs JavaScript Proxy - FileUpload
please use **HttpPostedFileBase** Type for fileupload, then the Proxy is created the right way. Please have a look on StackOverflow for FileUpload and AngularJs Directives.

    [CreateAngularJsProxy()]
    public ActionResult AddFileToServer(HttpPostedFileBase datei, int detailId)
    {
        //Do Something with the file            
        return Json(new Person() { Id = detailId}, JsonRequestBehavior.AllowGet);
    }

this will create the following prototype function in JavaScript

    proxyAngularJsSrv.prototype.addFileToServer = function (datei,detailId) { 
     var formData = new FormData(); 
     formData.append('datei', datei); 
       return this.http.post('Proxy/AddFileToServer'+ '?detailId='+detailId,formData, { transformRequest: angular.identity, headers: { 'Content-Type': undefined }}).then(function (result) {
            return result.data;
       });
    }


### Example: AngularJs TypeScript Proxy - CreateAngularTsProxyAttribute
Creates a proxy for AngularJs in TypeScript.

The .NET controller functions are decorated with the attribute "**CreateAngularTsProxyAttribute**" and you also need to add the "ReturnType" to the attribute params.
The "ReturnType" is the .NET type of the Json which is returned by the Json Function.

    using ProxyGenerator.ProxyTypeAttributes;

    public class ProxyController : Controller
    {  
        [CreateAngularTsProxy(ReturnType = typeof(Person))]
        public JsonResult AddTsEntryOnly(Person person)
        {
            return Json(person, JsonRequestBehavior.AllowGet);
        }

        [CreateAngularTsProxy(ReturnType = typeof(Auto))]
        public JsonResult AddTsEntryAndName(Person person, string name)
        {
            return Json(new Auto() { Marke = name}, JsonRequestBehavior.AllowGet);
        }

        [CreateAngularTsProxy(ReturnType = typeof(Auto))]
        public JsonResult AddTsEntryAndParams(Person person, string name, string vorname)
        {
            return Json(new Auto() { Marke = name}, JsonRequestBehavior.AllowGet);
        }

        [CreateAngularTsProxy(ReturnType = typeof(Person))]
        public JsonResult LoadTsCallById(int id)
        {
            return Json(new Person() { Id = id}, JsonRequestBehavior.AllowGet);
        }

        [CreateAngularTsProxy(ReturnType = typeof(Person))]
        public JsonResult LoadTsCallByParams(string name, string vorname, int alter)
        {
            return Json(new Person() { Name = name, Id = alter}, JsonRequestBehavior.AllowGet);
        }

        [CreateAngularTsProxy(ReturnType = typeof(Auto))]
        public JsonResult LoadTsCallByParamsAndId(string name, string vorname, int alter, int id)
        {
            return Json(new Auto() { Alter = alter, Marke = name}, JsonRequestBehavior.AllowGet);
        }

        [CreateAngularTsProxy(ReturnType = typeof(Auto))]
        public JsonResult LoadTsCallByParamsWithEnum(string name, string vorname, int alter, ClientAccess access)
        {
            return Json(new Auto() { Marke = name, Alter = alter}, JsonRequestBehavior.AllowGet);
        }

        [CreateAngularTsProxy(ReturnType = typeof(List<Auto>))]
        public JsonResult LoadAllAutosListe(string name)
        {
            return Json(new List<Auto>() { new Auto() { Marke = name}, new Auto() }, JsonRequestBehavior.AllowGet);
        }

        [CreateAngularTsProxy(ReturnType = typeof(Auto[]))]
        public JsonResult LoadAllAutosArray(string name)
        {
            return Json(new List<Auto>() { new Auto() { Marke = name}, new Auto() }.ToArray(), JsonRequestBehavior.AllowGet);
        }

        [CreateAngularTsProxy(ReturnType = typeof(Person))]
        public JsonResult ClearTsCall()
        {
            return Json(new Person(), JsonRequestBehavior.AllowGet);
        }

        [CreateAngularTsProxy(ReturnType = typeof(void))]
        public JsonResult VoidTsReturnType(string name)
        {
            return Json(string.Empty, JsonRequestBehavior.AllowGet);
        }

        [CreateAngularTsProxy(ReturnType = typeof(string))]
        public JsonResult StringTsReturnType(string name)
        {
            return Json(name, JsonRequestBehavior.AllowGet);
        } 

        [CreateAngularTsProxy(CreateWindowLocationHrefLink = true)]
        public FileResult GetDownloadSimple(int companyId, string name)
        {
            var fileContent = Encoding.ASCII.GetBytes(string.Format("This is a download for the CompanyId: {0} with the Name: {1}", companyId, name));
            return File(fileContent, "text/text", "TestDL.txt");
        }
    }

 this will create the following AngularJs TypeScript proxy. With an interface and the right "ReturnTypes" for each proxy call, please install TypeLite to create the TypeScript interfaces for each type. 
 
**Hint:** Also TypeLite uses attributes to create the interfaces for your classes, you need to set the "[TsClass]" attribute on the classes which are returned by your Json Calls to create the interfaces with TypeLite T4 template.

How to use the TypeScript module, take a look at my the GitHub code for this Project.

    module App.Services { 
       export interface IProxyPSrv { 
         addTsEntryOnly(person: ProxyGeneratorDemoPage.Models.Person.Models.IPerson) : ng.IPromise<ProxyGeneratorDemoPage.Models.Person.Models.IPerson>;
         addTsEntryAndName(person: ProxyGeneratorDemoPage.Models.Person.Models.IPerson,name: string) : ng.IPromise<ProxyGeneratorDemoPage.Models.Person.Models.IAuto>;
         addTsEntryAndParams(person: ProxyGeneratorDemoPage.Models.Person.Models.IPerson,name: string,vorname: string) : ng.IPromise<ProxyGeneratorDemoPage.Models.Person.Models.IAuto>;
         loadTsCallById(id: number) : ng.IPromise<ProxyGeneratorDemoPage.Models.Person.Models.IPerson>;
         loadTsCallByParams(name: string,vorname: string,alter: number) : ng.IPromise<ProxyGeneratorDemoPage.Models.Person.Models.IPerson>;
         loadTsCallByParamsAndId(name: string,vorname: string,alter: number,id: number) : ng.IPromise<ProxyGeneratorDemoPage.Models.Person.Models.IAuto>;
         loadTsCallByParamsWithEnum(name: string,vorname: string,alter: number,access: ProxyGeneratorDemoPage.Helper.ClientAccess) : ng.IPromise<ProxyGeneratorDemoPage.Models.Person.Models.IAuto>;
         loadAllAutosListe(name: string) : ng.IPromise<ProxyGeneratorDemoPage.Models.Person.Models.IAuto[]>;
         loadAllAutosArray(name: string) : ng.IPromise<ProxyGeneratorDemoPage.Models.Person.Models.IAuto[]>;
         clearTsCall() : ng.IPromise<ProxyGeneratorDemoPage.Models.Person.Models.IPerson>;
         voidTsReturnType(name: string): void;
         stringTsReturnType(name: string) : ng.IPromise<string>;
       
       
       export class ProxyPSrv implements IProxyPSrv {
         static $inject = ['$http']; 
         constructor(private $http: ng.IHttpService) { } 
       
         public addTsEntryOnly(person: ProxyGeneratorDemoPage.Models.Person.Models.IPerson) : ng.IPromise<ProxyGeneratorDemoPage.Models.Person.Models.IPerson> { 
             return this.$http.post('Proxy/AddTsEntryOnly',person).then((response: ng.IHttpPromiseCallbackArg<ProxyGeneratorDemoPage.Models.Person.Models.IPerson>) : ProxyGeneratorDemoPage.Models.Person.Models.IPerson => {return response.data;});
         } 
         
         public addTsEntryAndName(person: ProxyGeneratorDemoPage.Models.Person.Models.IPerson,name: string) : ng.IPromise<ProxyGeneratorDemoPage.Models.Person.Models.IAuto> { 
             return this.$http.post('Proxy/AddTsEntryAndName'+ '?name='+encodeURIComponent(name),person).then((response: ng.IHttpPromiseCallbackArg<ProxyGeneratorDemoPage.Models.Person.Models.IAuto>) : ProxyGeneratorDemoPage.Models.Person.Models.IAuto => {return response.data;});
         } 
         
         public addTsEntryAndParams(person: ProxyGeneratorDemoPage.Models.Person.Models.IPerson,name: string,vorname: string) : ng.IPromise<ProxyGeneratorDemoPage.Models.Person.Models.IAuto> { 
             return this.$http.post('Proxy/AddTsEntryAndParams'+ '?name='+encodeURIComponent(name)+'&vorname='+encodeURIComponent(vorname),person).then((response: ng.IHttpPromiseCallbackArg<ProxyGeneratorDemoPage.Models.Person.Models.IAuto>) : ProxyGeneratorDemoPage.Models.Person.Models.IAuto => {return response.data;});
         } 
         
         public loadTsCallById(id: number) : ng.IPromise<ProxyGeneratorDemoPage.Models.Person.Models.IPerson> { 
             return this.$http.get('Proxy/LoadTsCallById' + '/' + id).then((response: ng.IHttpPromiseCallbackArg<ProxyGeneratorDemoPage.Models.Person.Models.IPerson>) : ProxyGeneratorDemoPage.Models.Person.Models.IPerson => {return response.data;});
         } 
         
         public loadTsCallByParams(name: string,vorname: string,alter: number) : ng.IPromise<ProxyGeneratorDemoPage.Models.Person.Models.IPerson> { 
             return this.$http.get('Proxy/LoadTsCallByParams'+ '?name='+encodeURIComponent(name)+'&vorname='+encodeURIComponent(vorname)+'&alter='+alter).then((response: ng.IHttpPromiseCallbackArg<ProxyGeneratorDemoPage.Models.Person.Models.IPerson>) : ProxyGeneratorDemoPage.Models.Person.Models.IPerson => {return response.data;});
         } 
         
         public loadTsCallByParamsAndId(name: string,vorname: string,alter: number,id: number) : ng.IPromise<ProxyGeneratorDemoPage.Models.Person.Models.IAuto> { 
             return this.$http.get('Proxy/LoadTsCallByParamsAndId' + '/' + id+ '?name='+encodeURIComponent(name)+'&vorname='+encodeURIComponent(vorname)+'&alter='+alter).then((response: ng.IHttpPromiseCallbackArg<ProxyGeneratorDemoPage.Models.Person.Models.IAuto>) : ProxyGeneratorDemoPage.Models.Person.Models.IAuto => {return response.data;});
         } 
         
         public loadTsCallByParamsWithEnum(name: string,vorname: string,alter: number,access: ProxyGeneratorDemoPage.Helper.ClientAccess) : ng.IPromise<ProxyGeneratorDemoPage.Models.Person.Models.IAuto> { 
             return this.$http.get('Proxy/LoadTsCallByParamsWithEnum'+ '?name='+encodeURIComponent(name)+'&vorname='+encodeURIComponent(vorname)+'&alter='+alter+'&access='+access).then((response: ng.IHttpPromiseCallbackArg<ProxyGeneratorDemoPage.Models.Person.Models.IAuto>) : ProxyGeneratorDemoPage.Models.Person.Models.IAuto => {return response.data;});
         } 
         
         public loadAllAutosListe(name: string) : ng.IPromise<ProxyGeneratorDemoPage.Models.Person.Models.IAuto[]> { 
             return this.$http.get('Proxy/LoadAllAutosListe'+ '?name='+encodeURIComponent(name)).then((response: ng.IHttpPromiseCallbackArg<ProxyGeneratorDemoPage.Models.Person.Models.IAuto[]>) : ProxyGeneratorDemoPage.Models.Person.Models.IAuto[] => {return response.data;});
         } 
         
         public loadAllAutosArray(name: string) : ng.IPromise<ProxyGeneratorDemoPage.Models.Person.Models.IAuto[]> { 
             return this.$http.get('Proxy/LoadAllAutosArray'+ '?name='+encodeURIComponent(name)).then((response: ng.IHttpPromiseCallbackArg<ProxyGeneratorDemoPage.Models.Person.Models.IAuto[]>) : ProxyGeneratorDemoPage.Models.Person.Models.IAuto[] => {return response.data;});
         } 
         
         public clearTsCall() : ng.IPromise<ProxyGeneratorDemoPage.Models.Person.Models.IPerson> { 
             return this.$http.get('Proxy/ClearTsCall').then((response: ng.IHttpPromiseCallbackArg<ProxyGeneratorDemoPage.Models.Person.Models.IPerson>) : ProxyGeneratorDemoPage.Models.Person.Models.IPerson => {return response.data;});
         } 
         
         public voidTsReturnType(name: string) : void  { 
             this.$http.get('Proxy/VoidTsReturnType'+ '?name='+encodeURIComponent(name)); 
          } 
         
         public stringTsReturnType(name: string) : ng.IPromise<string> { 
             return this.$http.get('Proxy/StringTsReturnType'+ '?name='+encodeURIComponent(name)).then((response: ng.IHttpPromiseCallbackArg<string>) : string => {return response.data;});
         } 

         public getDownloadSimple(companyId: number,name: string) : void  { 
            window.location.href = 'Proxy/GetDownloadSimple'+ '?companyId='+companyId+'&name='+encodeURIComponent(name); 
         } 
       
       //#region Angular Module Definition 
         private static _module: ng.IModule; 
         public static get module(): ng.IModule {
             if (this._module) { return this._module; }
             this._module = angular.module('ProxyPSrv', []);
             this._module.service('ProxyPSrv', ProxyPSrv);
             return this._module; 
          }
        //#endregion 
          } 
     }

#### Example for AngularJs TypeScript Proxy - FileUpload
please use **HttpPostedFileBase** Type for fileupload, then the Proxy is created the right way. Please have a look on StackOverflow for FileUpload and AngularJs Directives.

    [CreateAngularTsProxy(ReturnType = typeof(Person))]
    public ActionResult AddFileToServer(HttpPostedFileBase datei, int detailId)
    {
        //Do Something with the file            
        return Json(new Person() { Id = detailId}, JsonRequestBehavior.AllowGet);
    }

this will create the following function in TypeScript

    public addFileToServer(datei: any,detailId: number) : ng.IPromise<ProxyGeneratorDemoPage.Models.Person.Models.IPerson> { 
        var formData = new FormData(); 
        formData.append('datei', datei); 
        return this.$http.post('Proxy/AddFileToServer'+ '?detailId='+detailId,formData, { 
            transformRequest: angular.identity, 
            headers: { 'Content-Type': undefined }
        }).then((response: ng.IHttpPromiseCallbackArg<ProxyGeneratorDemoPage.Models.Person.Models.IPerson>) : ProxyGeneratorDemoPage.Models.Person.Models.IPerson =>{return response.data;});
    } 


### Example: jQuery JavaScript Proxy - CreateJQueryJsProxyAttribute
Creates a proxy for jQuery in JavaScript.

The .NET controller functions are decorated with the attribute "**CreateJQueryJsProxyAttribute**"

    using ProxyGenerator.ProxyTypeAttributes;

    public class ProxyController : Controller
    { 
        [CreateJQueryJsProxy]
        public JsonResult AddJsEntryOnly(Person person)
        {
            return Json(person, JsonRequestBehavior.AllowGet);
        }

        [CreateJQueryJsProxy]
        public JsonResult AddJsEntryAndName(Person person, string name)
        {
            return Json(new Auto() { Marke =  name}, JsonRequestBehavior.AllowGet);
        }

        [CreateJQueryJsProxy]
        public JsonResult AddJsEntryAndParams(Person person, string name, string vorname)
        {
            return Json(new Auto() { Marke = name}, JsonRequestBehavior.AllowGet);
        }

        [CreateJQueryJsProxy]
        public JsonResult ClearJsCall()
        {
            return Json("ClearJsCall was Called", JsonRequestBehavior.AllowGet);
        }

        [CreateJQueryJsProxy(CreateWindowLocationHrefLink = true)]
        public FileResult GetDownloadSimple(int companyId, string name)
        {
            var fileContent = Encoding.ASCII.GetBytes(string.Format("This is a download for the CompanyId: {0} with the Name: {1}", companyId, name));
            return File(fileContent, "text/text", "TestDL.txt");
        }
    }
        
this will create the following jQuery JavaScript proxy directly localted in VS "below" the T4 template.

    window.proxyjQueryJs = function() { } 
    
    proxyjQueryJs.prototype.addJsEntryOnly = function (person) { 
        return jQuery.ajax( { url : 'Proxy/AddJsEntryOnly', data : JSON.stringify(person), type : "POST", contentType: "application/json; charset=utf-8" }).then(function (result) {
            return result;
       });
    }
    
    proxyjQueryJs.prototype.addJsEntryAndName = function (person,name) { 
        return jQuery.ajax( { url : 'Proxy/AddJsEntryAndName'+ '?name='+encodeURIComponent(name), data : JSON.stringify(person), type : "POST", contentType: "application/json; charset=utf-8" }).then(function (result) {
            return result;
       });
    }
    
    proxyjQueryJs.prototype.addJsEntryAndParams = function (person,name,vorname) { 
        return jQuery.ajax( { url : 'Proxy/AddJsEntryAndParams'+ '?name='+encodeURIComponent(name)+'&vorname='+encodeURIComponent(vorname), data : JSON.stringify(person), type : "POST", contentType: "application/json; charset=utf-8" }).then(function (result) {
            return result;
       });
    }
    
    proxyjQueryJs.prototype.clearJsCall = function () { 
        return jQuery.get('Proxy/ClearJsCall').then(function (result) {
            return result;
       });
    }

    proxyjQueryJs.prototype.getDownloadSimple = function (companyId,name) { 
        window.location.href = 'Proxy/GetDownloadSimple'+ '?companyId='+companyId+'&name='+encodeURIComponent(name) 
    } 

#### Example for jQuery JavaScript Proxy - FileUpload
please use **HttpPostedFileBase** Type for fileupload, then the Proxy is created the right way.

    [CreateJQueryJsProxy]
    public ActionResult AddFileToServer(HttpPostedFileBase datei, int detailId)
    {
        //Do Something with the file            
        return Json(new Person() { Id = detailId}, JsonRequestBehavior.AllowGet);
    }

this will create the following function in JavaScript

    proxyjQueryJs.prototype.addFileToServer = function (datei,detailId) { 
        var formData = new FormData(); 
        formData.append('datei', datei); 
        return jQuery.ajax( {
                             url : 'Proxy/AddFileToServer'+ '?detailId='+detailId,
                             data : formData, 
                             processData : false,
                             contentType: false, 
                             type : "POST" })
                    .then(function (result) {
                        return result;
                    });
    }

### Example: jQuery TypeScript Proxy - CreateJQueryTsProxyAttribute
Creates a proxy for jQuery in TypeScript.

The .NET controller functions are decorated with the attribute "**CreateJQueryTsProxyAttribute**" and you also need to add the "ReturnType" to the attribute params.
The "ReturnType" is the .NET type of the Json which is returned by the Json Function.

    using ProxyGenerator.ProxyTypeAttributes;

    public class ProxyController : Controller
    { 
        [CreateJQueryTsProxy(ReturnType = typeof(Person))]
        public JsonResult AddTsEntryOnly(Person person)
        {
            return Json(person, JsonRequestBehavior.AllowGet);
        }

        [CreateJQueryTsProxy(ReturnType = typeof(Auto))]
        public JsonResult AddTsEntryAndName(Person person, string name)
        {
            return Json(new Auto() { Marke = name}, JsonRequestBehavior.AllowGet);
        }

        [CreateJQueryTsProxy(ReturnType = typeof(Auto))]
        public JsonResult AddTsEntryAndParams(Person person, string name, string vorname)
        {
            return Json(new Auto() { Marke = name}, JsonRequestBehavior.AllowGet);
        }

        [CreateJQueryTsProxy(ReturnType = typeof(Person))]
        public JsonResult LoadTsCallById(int id)
        {
            return Json(new Person() { Id = id}, JsonRequestBehavior.AllowGet);
        }

        //Returns HTML Template as pure String/HTML
        [CreateJQueryTsProxy(ReturnType = typeof(string))]
        public ActionResult TestView()
        {
            return View();
        }

        [CreateJQueryTsProxy(CreateWindowLocationHrefLink = true)]
        public FileResult GetDownloadSimple(int companyId, string name)
        {
            var fileContent = Encoding.ASCII.GetBytes(string.Format("This is a download for the CompanyId: {0} with the Name: {1}", companyId, name));
            return File(fileContent, "text/text", "TestDL.txt");
        }
    }

 this will create the following jQuery TypeScript proxy. With an interface and the right "ReturnTypes" for each proxy call, please install TypeLite to create the TypeScript interfaces for each type. 

      module App.JqueryServices { 
        export interface IProxyjQueryTs { 
            testView() : JQueryPromise<string>;
            addTsEntryOnly(person: ProxyGeneratorDemoPage.Models.Person.Models.IPerson) : JQueryPromise<ProxyGeneratorDemoPage.Models.Person.Models.IPerson>;
            addTsEntryAndName(person: ProxyGeneratorDemoPage.Models.Person.Models.IPerson,name: string) : JQueryPromise<ProxyGeneratorDemoPage.Models.Person.Models.IAuto>;
            addTsEntryAndParams(person: ProxyGeneratorDemoPage.Models.Person.Models.IPerson,name: string,vorname: string) : JQueryPromise<ProxyGeneratorDemoPage.Models.Person.Models.IAuto>;
            loadTsCallById(id: number) : JQueryPromise<ProxyGeneratorDemoPage.Models.Person.Models.IPerson>;
        }
        
        export class ProxyjQueryTs implements IProxyjQueryTs {
             public testView() : JQueryPromise<string> { 
                 return jQuery.get('Proxy/TestView').then((result: string) : string => { return result; });
            } 

            public addTsEntryOnly(person: ProxyGeneratorDemoPage.Models.Person.Models.IPerson) : JQueryPromise<ProxyGeneratorDemoPage.Models.Person.Models.IPerson> { 
                return jQuery.ajax( { url : 'Proxy/AddTsEntryOnly', data : JSON.stringify(person), type : "POST", contentType: "application/json; charset=utf-8" }).then((result: ProxyGeneratorDemoPage.Models.Person.Models.IPerson) : ProxyGeneratorDemoPage.Models.Person.Models.IPerson=>{return result;});
            } 

            public addTsEntryAndName(person: ProxyGeneratorDemoPage.Models.Person.Models.IPerson,name: string) : JQueryPromise<ProxyGeneratorDemoPage.Models.Person.Models.IAuto> { 
                return jQuery.ajax( { url : 'Proxy/AddTsEntryAndName'+ '?name='+encodeURIComponent(name), data : JSON.stringify(person), type : "POST", contentType: "application/json; charset=utf-8" }).then((result: ProxyGeneratorDemoPage.Models.Person.Models.IAuto) : ProxyGeneratorDemoPage.Models.Person.Models.IAuto=>{return result;});
            } 

            public addTsEntryAndParams(person: ProxyGeneratorDemoPage.Models.Person.Models.IPerson,name: string,vorname: string) : JQueryPromise<ProxyGeneratorDemoPage.Models.Person.Models.IAuto> { 
                return jQuery.ajax( { url : 'Proxy/AddTsEntryAndParams'+ '?name='+encodeURIComponent(name)+'&vorname='+encodeURIComponent(vorname), data : JSON.stringify(person), type : "POST", contentType: "application/json; charset=utf-8" }).then((result: ProxyGeneratorDemoPage.Models.Person.Models.IAuto) : ProxyGeneratorDemoPage.Models.Person.Models.IAuto=>{return result;});
            } 

            public loadTsCallById(id: number) : JQueryPromise<ProxyGeneratorDemoPage.Models.Person.Models.IPerson> { 
                return jQuery.get('Proxy/LoadTsCallById' + '/' + id).then((result: ProxyGeneratorDemoPage.Models.Person.Models.IPerson) : ProxyGeneratorDemoPage.Models.Person.Models.IPerson=>{return result;});
            } 

            public getDownloadSimple(companyId: number,name: string) : void  { 
               window.location.href = 'Proxy/GetDownloadSimple'+ '?companyId='+companyId+'&name='+encodeURIComponent(name); 
            } 
        }
    }

#### Example for jQuery TypeScript Proxy - FileUpload
please use **HttpPostedFileBase** Type for fileupload, then the Proxy is created the right way.

    [CreateJQueryTsProxy(ReturnType = typeof(Person))]
    public ActionResult AddFileToServer(HttpPostedFileBase datei, int detailId)
    {
        //Do Something with the file            
        return Json(new Person() { Id = detailId}, JsonRequestBehavior.AllowGet);
    }

this will create the following function in TypeScript

    public addFileToServer(datei: any, detailId: number): JQueryPromise<ProxyGeneratorDemoPage.Models.Person.Models.IPerson> {
        var formData = new FormData();
        formData.append('datei', datei);
        return jQuery.ajax({ 
                              url: 'Proxy/AddFileToServer' + '?detailId=' + detailId,
                              data: formData, processData: false, 
                              contentType: false, 
                              type: "POST" })
                     .then((result: ProxyGeneratorDemoPage.Models.Person.Models.IPerson): ProxyGeneratorDemoPage.Models.Person.Models.IPerson =>{return result;});
    } 

## Debugging Output
When you need some logoutput, then you only need to change the following line in the "ProxyGenerator.tt" from

      <#=generator.Factory.GetLogManager().GetCompleteLogAsString(false) #>

to

      <#=generator.Factory.GetLogManager().GetCompleteLogAsString(true) #>

the "true" will generate all the errors directly into each proxy file as comment at the beginning of each Proxy. When you have multiple proxies the error message is
generated in every file and its in every proxy the SAME MESSAGE.

## Known Errormessages
### 1. Method/Function overload not supported
It is not possible to use function overload in JavaScript and so its also not possible for the template to create proxy calls when you have set the attribut on both overloaded functions.

        [CreateAngularJsProxy]
        public JsonResult AddJsEntryAndParams(Person person, string name, string vorname)
        {
            return Json(new Auto() { Marke = name}, JsonRequestBehavior.AllowGet);
        }

        [CreateAngularJsProxy]
        public JsonResult AddJsEntryAndParams(Person person, string name)
        {
            return Json(new Auto() { Marke = name }, JsonRequestBehavior.AllowGet);
        }

    
Then you get the following errormessage, when you try to create the proxy 

**ERROR, JavaScript doesn't supports function/method overload, please rename one of those functions/methods 'AddJsEntryAndParams'.**

### 2. The "WebProjectName" was not found
When you have set the wrong "WebProjectName" in the ProxySettings, then the following Error will appear, when you try to create the proxies.

**The 'WebProjectPath' was not found, because the 'WebProjectName' was wrong.**

## How to get the DemoPageProxy Generator Running
the above github repository contains a .NET Solution with an MVC Page. If the Project "NuGet.Packager" can't be loaded its no problem its just for building the NuGet packages.

You need to restore the NuGet packages and also the npm modules for the solution and install the es6-shim Typings.

I've created a german blogpost how to get Angular 2 running with VS 2015 and ASP.Net MVC 

https://squadwuschel.wordpress.com/2016/04/01/angular-2-hello-world-mit-visual-studio-2015-update-2-asp-net-4-und-typescript/

perhaps this post can help you if you can't get the Solution running.
