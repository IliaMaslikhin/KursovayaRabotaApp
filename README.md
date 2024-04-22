# 
Тема: Разработка объектной программы для задачи «Книжное издательство»

Постановка задачи.

Книжное издательство ведет список своих авторов с указанием уникальной фамилии автора и номера его мобильного телефона. Для каждого автора создается список его книг с указанием названия книги и ее тиража.

Разработка программы включает в себя:

Определение необходимых объектов и способов их взаимодействия

Формальное описание объектов в виде классов

Программную реализацию всех необходимых методов, включая подсчет суммарного тиража как по каждому автору, так и в целом по всему издательству

Всестороннее тестирование методов с помощью консольного (при разработке) и оконного (в окончательном варианте) приложения.

Для объединения авторов используется структура данных в виде очереди на основе динамического массива со сдвигом элементов. 
Для объединения книг у каждого автора используется структура данных в виде адресного разомкнутого неупорядоченного однонаправленного списка с заголовком.

 

Разработка выполняется с учетом следующих требований:

Имена классов, свойств и методов должны носить содержательный смысл и соответствовать информационной задаче

Обязательное соблюдение принципа инкапсуляции – использование в классах только закрытых свойств и реализация необходимого набора методов доступа

Наличие двух методов для сохранения всей объектной структуры во внешнем файле с обратной загрузкой, при этом стандартные механизмы сериализации разрешается использовать только как дополнение к самостоятельно реализованным методам

Тестовое оконное приложение должно обладать удобным пользовательским интерфейсом с контролем вводимых данных и отображением текущего состояния объектной структуры с помощью табличных компонентов

Cтандартные контейнеры/коллекции (включая обобщенные классы) разрешается использовать только как дополнение к самостоятельно разработанным классам  
#