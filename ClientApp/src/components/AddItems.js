import React, { Component } from 'react';
import { saveItemsDataFetch } from '../api/fetchItemsAPI';

export class AddItems extends Component {
    static displayName = AddItems.name;

    constructor(props) {
        super(props);
        this.state = {
            jsonData: ''
        };
    }

    renderTable() {
        return (
            <div style={inputData.flexColumn}>
                <div> Input data in JSON format </div>
                <textarea
                    style={inputData.margin}
                    name="inputData"
                    rows="4"
                    cols="50"
                    value={this.state.jsonData}
                    onChange={e => this.setState({ jsonData: e.target.value })}>
                </textarea >
                <button onClick={this.saveData.bind(this)}>
                    Save data
                </button>
            </div>
        );
    }

    render() {
        return (
            <div>
                {this.renderTable()}
            </div>
        );
    }

    isValidateInput(event) {
        try {
            JSON.parse(event);
        } catch (e) {
            return false;
        }
        return true;
    }

    clearFilters() {
        this.setState({ jsonData: '' });
    }

    async saveData() {
        const jsonData = this.state.jsonData;

        if (!this.isValidateInput(jsonData)) {
            alert('Invalid JSON! Please check data');
            this.setState({ jsonData: '' });
            return;
        }

        await saveItemsDataFetch(jsonData);

        this.state.jsonData = '';
        this.getItemsData('', '');
    }
}

const inputData = {
    flexColumn: {
        padding: "0.5em",
        display: "flex",
        flexDirection: "column",
    },
    flexRow: {
        padding: "0.5em",
        display: "flex",
        flexDirection: "row",
        justifyContent: "start",
    },
    margin: {
        margin: "0.5em 0"
    }
}
