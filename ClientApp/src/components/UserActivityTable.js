import React, { Component } from 'react';
import { UserActivityElement } from './UserActivityElement';
import './UserActivity.css';

export class UserActivityTable extends Component {

    constructor(props) {
        super(props)
        this.handleChange = this.handleChange.bind(this);
        this.handleSaveClick = this.handleSaveClick.bind(this);
        this.handleCalculateClick = this.handleCalculateClick.bind(this);
        this.state = {
            elements: [
                { dateRegistered: "2020-12-15", dateLastVisit: "2020-12-22" },
                { dateRegistered: "2020-12-15", dateLastVisit: "2020-12-20" },
                { dateRegistered: "2020-12-15", dateLastVisit: "2020-12-26" },
                { dateRegistered: "2020-12-15", dateLastVisit: "2020-12-25" },
                { dateRegistered: "2020-12-15", dateLastVisit: "2020-12-30" }
            ],
            rollingRetentionValue: null
        };
    }

    handleSaveClick(event) {
        
        fetch('api/UserActivities', {
            method: 'post',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(this.state.elements)
        })
        .then(response => {
            if (response.status !== 200) {
                alert('Произошла ошибка во время сохранения данных ' + response.status);
            } else {
                alert('Данные успешно сохранены.');
                // сбрасываем значение Rolling Retention после очередного сохранения данных. Для расчета требуется нажать кнопку Calculate.
                this.setState({ rollingRetentionValue: null });
            }            
        })
        .catch(err => {
            console.log(`error: ${err}`);
        });        
    }

    handleCalculateClick(event) {
        
        fetch('api/UserActivities/GetUsersMetrics')
            .then(response => {
                if (response.status !== 200) {                    
                    alert('Произошла ошибка во время расчета Rolling Retention ' + response.status);
                }

                response.json().then(data => {
                    this.setState({ rollingRetentionValue: data });
                });
            })
            .catch(err => {
                console.log(`error: ${err}`);
            });
    }

    handleChange(event) {
        
        var elements = this.state.elements.slice(); 
        let index = event.target.getAttribute('data-index');
        let alias = event.target.getAttribute('data-alias');
        elements[index][alias] = event.target.value; 
        this.setState({ elements: elements }); 
    }

    render() {

        const userDataElements = this.state.elements.map((item, index) => {
            return (
                <UserActivityElement
                    key={index}
                    index={index}
                    dateRegistered={item.dateRegistered}
                    dateLastVisit={item.dateLastVisit}
                    onChange={this.handleChange} />
            );
        });

        return (
            <>
                <table>
                    <thead>
                        <tr>
                            <th>
                                Date Registered
                            </th>
                            <th>
                                Date Last Visit
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        {userDataElements}
                    </tbody>
                </table>
                <div class="buttons-container">
                    <button class="button" onClick={this.handleSaveClick}>Save</button>
                    <button class="button" onClick={this.handleCalculateClick}>Calculate</button>
                </div>
                {this.state.rollingRetentionValue ? (
                    <div>Rolling Retention 7 day = {this.state.rollingRetentionValue}</div>)
                    : ("")}
            </>
        );
    }
}