# homework-6

Вашему коллеге поставили задачу реализовать сервис поиска фильмов в котором вместе играют два актера.  
У сервиса есть один метод API: `POST /ActorsMatch`, который на вход принимает Json вида  
```javascript
{
  "Actor1":"Keanu Reeves",             // Обязательный парамерт
  "Actor2": "Winona Ryder",            // Обязательный парамерт
  "MoviesOnly": true,                  // Необязательный параметр
}
```   
Где  
* Actor1 - первый актер  
* Actor2 - второй актер  
* MoviesOnly - вернуть только фильмы, то есть не озвучка и не телешоу

Пример ответа json:
```javascript
[
    "Destination Wedding",
    "The Private Lives of Pippa Lee",
    "A Scanner Darkly",
    "Bram Stoker's Dracula"
]
``` 
Данные необходимо брать из https://imdb-api.com, для этого нужно там зарегистироваться и получить api_key. На бесплатном плане можно делать до 100 запросов в день.  
Ваш коллега перед уходом в отпуск успел реализовать логику поиска совместных фильмов, а также закешировал запросы поиска ID актера по его имени в БД actorsdb, которая содержит всего одну таблиц actors  
```sql
create table actors
(
  name     varchar(100) not null,
  actor_id varchar(100) not null
);
```  
В этой таблице хранятся имя актера и его идентификатор.  
Вам необходимо:  
* Отрефакторить код согласно принципам чистой архитектуры
* Сделать поиск с учетом фильтра MoviesOnly (для этого нужно выбирать только те фильмы, у которых Role это "Actress" или "Actor"
