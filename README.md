# StockDemo

## How to build and debug the code

首先下载源码，使用VS 2019 打开解决方案文件 `..\StockDemo\StockDemo.sln`。 本DEMO因为时间有限，为方便起见，采用MVC做前端开发，设置 `StockDemoMvc`为起始项目，点击DEBUG即可调试项目。

## The Test Screenshot

功能测试截图请参阅 TestScreenshot 文件夹

## Code Document

本项目采用.net 5 开发

项目架构分为：
Entities
Infrastructure
Services
Gateway
Test

### Entities

实体分为DTO、DO、Enum

DO （Domain Object） 包含 BaseCalculation（用于工厂类生成股票扩展计算模型），BaseStock（股票基类），CalculationModel（用于计算和存储上下文模型）

DTO （Data Transfer Object） 用于数据传输和外部对接模型

Enum 用于对系统的标准进行定义 如 股票代码

### Infrastructure

Infrastructure 提供了系统的基础架构包括 泛型工厂，策略类，仓储与存储

### Service

通过计算生成计算模型，并通过二级缓存提升查询效率

### Gateway

网关层提供了API进行数据查询，具备了SPA 架构 APP的数据查询能力。因本DEMO 由于时间原因，没有采用SPA APP（如VUE，React），而是采用了MVC架构。

### Test

使用NUnit，Moq。 包含数据测试，单元测试，服务集成测试，系统测试（详见TestScreenshot 文件夹）

## Technology Points

设计模式：单例（存储），泛型工厂（用于生成计算模型），策略（用于替换算法）
架构：内存存储，二级缓存，MVC
技术：EChart，MemoryCache，Web API, MVC，Moq，Csv,Ajax
