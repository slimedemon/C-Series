1. Basic about application domains 
* AppDomains are a key aspect of the OS-neutral nature ò the .NET platform.
* AppDomains are far less expensive in terms of processing power and memory than a full-blown process.
* AppDomains provide a deeper level of isolation for hosting a loaded application.

2. The System.AppDomain class
--------- Method -----------
* CreateDomain()
* CreateInstace()
* ExecuteAssembly()
* GetAssemblies()
* GetCurrentThreadId()
* Load()
* Unload()

------- Properties -----------
* BaseDirectory
* CurrentDomain
* FriendlyName
* MonitoringIsEnabled
* SetupInformation

------- Events -----------
* AssemblyLoad
* AssemblyResolve
* DomainUnload
* FirstChangeException
* ProcessExit
* UnhandleException


