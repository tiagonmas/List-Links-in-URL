# Project Description
Command Line tool that will fetch all links refered in a given URL. It will enable to crawl the URLS inside the same domain, up to a given level, fetching as much externals URLs as it finds.


# Command Line Options:
-Help Displays this help text
-Max The number of internal links the crawler will follow (default is 1)
-URL Specifies the URL to look for (mandatory)

# Sample Execution
```
C:\>ListLinks.exe -url=http://autos.yahoo.com/ -max=1

List Links version 1.0.0.0
Lists all links that exist in a given page

Looking for links in http://autos.yahoo.com/
CRAWLING http://autos.yahoo.com/ level 1
...............

Dumping Found URLS:
https://edit.yahoo.com/config/eval_register?.src=auto&.intl=us&.lang=en-US&.done=http://autos.yahoo.
com/
https://login.yahoo.com/config/login?.src=auto&.intl=us&.lang=en-US&.done=http://autos.yahoo.com/
http://help.yahoo.com/l/us/yahoo/autos/
http://us.lrd.yahoo.com/_ylc=X3oDMTFnNzFiMTJoBHRtX2RtZWNoA1RleHQgTGluawR0bV9sbmsDVTExMzA1NTYEdG1fbmV
0A1lhaG9vIQ--/SIG=112cgufir/http%3A//www.yahoo.com/bin/set
http://mail.yahoo.com/?.intl=us&.lang=en-US
http://my.yahoo.com/
http://www.yahoo.com/
http://www.adobe.com/products/flashplayer/
http://realestate.yahoo.com/
http://travel.yahoo.com/
http://shopping.yahoo.com/
http://local.yahoo.com/
http://downloads.yahoo.com/
http://suggestions.yahoo.com/?prop=autos
```
