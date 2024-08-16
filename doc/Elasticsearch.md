# Elasticsearch

## 如何重置密码

在 ElasticSearch 8.x 版本中，X-Pack Security 默认启用，ElasticSearch 会在首次启动时为内置的 `elastic` 用户生成一个随机密码。你可以通过以下方式查看或重置该密码：

### 1. 查看启动日志中的密码

当你首次启动 ElasticSearch 时，`elastic` 用户的初始密码会在启动日志中显示。如果你还可以访问容器的启动日志，你可以使用以下命令查看该密码：

```bash
docker logs <container_name>
```

替换 `<container_name>` 为你的 ElasticSearch 容器的名称，例如：

```bash
docker logs elasticsearch
```

在日志中，你会看到类似如下的信息：

```plaintext
...
-----------------------------------------------------------------
Password for the elastic user is: random_password_here
-----------------------------------------------------------------
...
```

这里的 `random_password_here` 就是为 `elastic` 用户生成的密码。

### 2. 重置 `elastic` 用户的密码

如果你无法找到日志中显示的初始密码，或者你想重置密码，你可以通过以下步骤来重置 `elastic` 用户的密码：

#### 2.1 使用 `elasticsearch-reset-password` 命令

进入 ElasticSearch 容器，然后使用 `elasticsearch-reset-password` 命令来重置密码：

```bash
docker exec -it <container_name> /bin/bash
```

然后执行：

```bash
elasticsearch-reset-password -u elastic
```

命令将提示你选择生成新密码或输入自定义密码。

#### 2.2 手动输入自定义密码

如果你想手动设置密码，可以在提示时选择自定义密码选项，然后输入你想要的密码。

#### 2.3 使用 API 重置密码

你也可以通过 ElasticSearch 的 `_security` API 来重置密码。首先，你需要通过 `curl` 命令或其他 HTTP 客户端发送以下请求：

```bash
curl -X POST "http://localhost:9200/_security/user/elastic/_password" -H "Content-Type: application/json" -d'
{
  "password" : "new_password_here"
}'
```

替换 `"new_password_here"` 为你想设置的新密码。

### 小结

ElasticSearch 在首次启动时会在日志中输出 `elastic` 用户的初始密码。如果无法找到该密码，可以使用 `elasticsearch-reset-password` 命令在容器中重置密码，或者通过 ElasticSearch 的 `_security` API 重置密码。确保你在操作完成后安全地保存新密码。