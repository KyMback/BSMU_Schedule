# План тестирования
## Содержание
1. [Введение](#1-Введение)  
2. [Объект тестирования](#2-Объект-тестирования)  
3. [Риски](#3-Риски)  
4. [Аспекты тестирования](#4-Аспекты-тестирования)  
5. [Подходы к тестированию](#5-Подходы-к-тестированию)  
6. [Представление результатов](#6-Представление-результатов)  
7. [Выводы](#7-Выводы)
## 1. Введение
Основная цель данного документа состоит в том, чтобы описать план тестирования мобильного-приложения "BSMU Schedule". Эта информация предназначена для команды, которая будет тестировать данное программное обеспечение на соответствие [требованиям](../SRS.md).
## 2. Объект тестирования
В качестве объекта тестирования следует выделить функциональное тестирование, а также тестирование удобства и простоты использования. К атрибутам качетсва относятся следующие характеристики:  
1. Функциональная корректность;  
2. Удобство использования;  
3. Защищенность от ошибок пользователя.
## 3. Риски
Приложение может обновлять расписание только при наличии у пользователя подключения к сети Интернет. К рискам, способным повлиять на работоспособность приложения можно отнести:
1. Низкая скорость или отсутсвие интернет-соединения на стороне пользователя;  
2. Низкая производительность устройства;
## 4. Аспекты тестирования
В процессе тестирования предполагается проверить соответствие системы требованиям, на основе которых она была спроектирована и реализована. Для данного проекта основными функциями являются следующие (функциональные требования):  
1. Возможность выбора группы.
2. Скачивание расписания.
3. Обновление расписания.
4. Просмотр учебных предметов по дням.

Также необходимо провести тестирование нефункциональных требований:
1. Удобство использования
* Единообразность вида всех страниц мобильного приложения
* Предоставление подписи дня недели на кнопках переключения дней
* Предоставление номера группы на главной странице
2. Внешние интерфейсы
* Красные тона на рамках приложения

## 5. Подходы к тестированию
Для тестирования приложения необходимо использовать ручное тестирование, чтобы проверить все [аспекты тестирования](#4-Аспекты-тестирования).
## 6. Представление результатов
Результаты тестирования представлены в документе ["Представление результатов"](TR.md)
## 7. Выводы
Данный тестовый план позволяет протестировать основной функционал приложения. Успешное прохождение всех тестов не гарантирует полной работоспособности на всех платформах и архитектурах, однако позволяет полагать, что данное программное обеспечение работает корректно и является удобным в использовании.