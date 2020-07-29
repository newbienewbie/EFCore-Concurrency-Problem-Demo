
A DEMO for Concurrency Problem

- 默认情况下：对于普通的select，会出现lost update问题
- 使用Repeatable isolation level, 也有几率出现lost update问题(同默认情况)。这是由于MYSQL需要使用 `select ... from ... for update` 而非普通的`select`来锁定该行([SO](https://stackoverflow.com/q/10040785/10091607)。
- 使用Serializable isolation level，未发现有此类问题，但是有很高几率失败，日志显示死锁