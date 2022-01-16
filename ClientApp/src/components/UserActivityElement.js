import React, { Component } from 'react';

export class UserActivityElement extends React.Component {

    constructor(props) {
        super(props);
        this.handleChange = this.handleChange.bind(this);
    }

    handleChange(event) {
        this.props.onChange(event);
    }

    render() {
        const dateRegistered = this.props.dateRegistered;
        const dateLastVisit = this.props.dateLastVisit;
        return (

            <tr>
                <td>
                    <input
                        type="date"
                        data-index={this.props.index}
                        data-alias="dateRegistered"
                        value={dateRegistered}
                        onChange={this.handleChange} />
                </td>
                <td>
                    <input
                        type="date"
                        data-index={this.props.index}
                        data-alias="dateLastVisit"
                        value={dateLastVisit}
                        onChange={this.handleChange} />
                </td>

            </tr>


        );
    }
}