# Session

## Session 实现机制

当用户登录成功后，生成一个Session，用户端会将Session保存在Cooke中，服务器端会将Session会保存在内存中。  
在下次发送请求的时候，带上这个Session，服务器就能判断是哪个用户发送的请求。  

Session不适合保存在服务器内存中，但是可以放在一个中心状态服务器，例如Redis，Memcached。  
不适合前后端分离项目，大系统，分布式系统，因为所有取Seesion的操作都要经过中心化服务器，性能有点问题。  

