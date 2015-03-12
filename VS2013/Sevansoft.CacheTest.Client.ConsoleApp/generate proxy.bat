@ECHO OFF

IF "%Program Files(x86)%" == "" GOTO X86ONLY
"C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\Bin\svcutil.exe" /language:vb /out:Proxy.vb /config:proxy.config http://sevansoft-wcfcache.azurewebsites.net/ProductService.Svc?wsdl http://sevansoft-wcfcache.azurewebsites.net/CountryInformationService.svc?wsdl
GOTO END

:X86ONLY
"C:\Program Files\Microsoft SDKs\Windows\v7.0A\Bin\svcutil.exe" /language:vb /out:Proxy.vb /config:proxy.config http://sevansoft-wcfcache.azurewebsites.net/ProductService.Svc?wsdl http://sevansoft-wcfcache.azurewebsites.net/CountryInformationService.svc?wsdl
GOTO END

:END
pause