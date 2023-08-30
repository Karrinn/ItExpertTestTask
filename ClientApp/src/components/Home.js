﻿import React, { Component } from 'react';

export class Home extends Component {
    static displayName = Home.name;

    render() {
        return (
            <div>
                <h1>
                    Тестовое задание It-Expert!
                </h1>
                <h4>
                    Тестовое задание 1
                </h4>
                <p>
                    Необходимо реализовать веб-приложение на Asp.Net Core.
                </p>
                <p>
                    Задание реализовано с помощью шаблона ASP.NET Core + React.
                    <br />
                    Я не стал делать задание на VueJS, хотел попробовать React, для фронт части можно было использовать библиотеку компонентов, например material-ui, но я не стал.
                    <br />
                    Для отображения данных в таблице и пагинации и фильтрации можно было бы использовать готовые компоненты. Для отображения ошибок использовать PopUp компонент. 
                    <br />
                    Я не стал использовать готовые компоненты из-за отсутствия времени.
                    <br />
                    <br />
                    В разделе <a href="/fetch-data">Fetch data</a> клиентская часть позволяет просмотреть данные и отфильтровать по полям.
                    <br />
                    Так же там можно сохранить данные, на входе должен быть валидный JSON текст, перед отправко происходит проверка валидности JSON.
                    <br />
                    Можно использовать следующий шаблон для проверки
                    <br />
                    <textarea rows='10' cols='40' value='[{"1": "value1"},
	                    {"1": "value2"},
	                    {"5": "value1"},
	                    {"5": "value2"},
	                    {"10": "value1"},
	                    {"10": "value2"}]'>
                    </textarea>
                    <br />
                    В качестве базы данных была выбрана SQLite
                </p>    
                <h4>
                    Тестовое задание 2
                </h4>
                <p>
                    Текст решенияз задача на SQL находится в файле Task_2.sql посмотреть можно по <a href="https://github.com/Karrinn/ItExpertTestTask/blob/master/Task_2.sql">ссылке</a>
                </p>
            </div>
        );
    }
}
