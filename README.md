# DataX
ТЗ :
Разработать приложение для наполнения базы данных соответсвия слов на русском словам на английском языке. Приложение включает в себя WCF сервис который добавляет слова в свой словарь, ищет перевод и клиентское приложение которое использует WCF сервис для добавления слов и получения перевода

Выполнение работы проводилось посредством инструментов :
Microsoft Visual Studio 2012, Среда Microsoft SQL Server Management Studio, Microsoft Expression Bland. 

Использованы технологии и фреймворки:
C#,Net 4.0 C# для WCF сервиса,WPF (MVVM фреймворк Caliburn.Micro), Entity Framework(Code First DB(many - to - many)).

Разработаны  3 приложения : 
DictonaryService – приложение , реализующее WCF – сервис по работе с
 клиентским приложением и работу с базой данных.
Client – клиентское приложение,реализованноное с помощью MVVM фреймворка Caliburn.Micro , позволяющее пользователю работать
 со англо – русским словарём :
добавлять, удалять, обновлять слова, производить поиск перевода.
UnitTestDictonaryService тестирование сервиса 2 способами : 
с помощью UnitTest , и с помощью Effort c созданием БД в ОЗУ ПК .
Для начала работы необходимо настроить сервис :
    в проекте DictonaryService находим, открываем  файл app.config
  в нём делаем изменения  в строке <connectionStrings>
 ![settingsappconfig](https://cloud.githubusercontent.com/assets/11519562/10878116/07461dcc-8165-11e5-87e4-7bf9339ccc4a.jpg)
Сохраняем, закрываем файл.
После этого запускаем DictonaryService – должен запуститься тестовый клиент WCF.

 ![testclientwcf](https://cloud.githubusercontent.com/assets/11519562/10878140/1c58f220-8165-11e5-9f28-7bfaa4c6e541.jpg)
 
Запускаем клиентское приложение Client

 ![connecttoserver](https://cloud.githubusercontent.com/assets/11519562/10878132/1c1bf0be-8165-11e5-8591-eeb4f92ead5f.jpg)

Поскольку у нас нет еще БД нам нужно добавить слова и после этого будет  она создана.
При попытке поиска любого слова , например «девочка» - получаем ответ от сервера :

 ![findword](https://cloud.githubusercontent.com/assets/11519562/10878135/1c34b1b2-8165-11e5-9691-3a519ed5af5d.jpg)
 
Нажимаем кнопку «Добавить слово» - открывается окно редактирования 

 ![addingword](https://cloud.githubusercontent.com/assets/11519562/10878131/1c194490-8165-11e5-9f15-24cc5b7720b8.jpg)

Вносим перевод .

 ![addingtranslate](https://cloud.githubusercontent.com/assets/11519562/10878130/1c11358e-8165-11e5-91b6-9781e7ea361b.jpg)

Нажимаем «Добавить слова в список», добавляем следующее.

 ![addingnextwordtranslate](https://cloud.githubusercontent.com/assets/11519562/10878141/1c605416-8165-11e5-9dee-f12fdbc08c44.jpg)
 
Нажимаем «Обновить изменения в БД»
 Можем увидеть с помощью приложения SQL Server Manangment Studio , что была создана наша БД.
 
 ![createddb](https://cloud.githubusercontent.com/assets/11519562/10878133/1c2762f0-8165-11e5-815e-27f200e5bff0.jpg)
 
Теперь задаём поиск слова «девочка»

 ![gettingtranslate](https://cloud.githubusercontent.com/assets/11519562/10878136/1c3e5b4a-8165-11e5-8218-6289c93e648d.jpg)
 
Задаём поиск слова «girl», выбираем словарь англо-русский

 ![searchenrustranslate](https://cloud.githubusercontent.com/assets/11519562/10878137/1c457fec-8165-11e5-90d2-018e76d0e506.jpg)
 
Задаём поиск слова «miss» , выбираем словарь англо-русский

 ![searchnextenrustranslate](https://cloud.githubusercontent.com/assets/11519562/10878138/1c4d36c4-8165-11e5-9a5e-723813ce4f73.jpg)
 
Нажимаем  -добавить слово – в открытом окне редактирования кликаем на слове «miss» - нажимаем удалить из списка – после этого нажимаем «Обновить изменения в БД».
Проверяем в поиске слово «девочка».
 
Слово «miss» было удалено .

![deleteword](https://cloud.githubusercontent.com/assets/11519562/10878134/1c2b804c-8165-11e5-8acd-6fcae4132d06.jpg)

В поле «Поисковое слово » вбиваем «девочка» - нажимаем «Удалить слово из БД»
 

Рад буду услышать вопросы и предложения по:
Моб. Тел. 050 341-75-84,
email : spread2009@gmail.com,
Skype : kpp2010,
С уважением Карпусь Павел.
