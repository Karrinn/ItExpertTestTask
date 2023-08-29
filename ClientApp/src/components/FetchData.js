import React, { Component } from 'react';
import { saveItemsDataFetch, getItemsDataFetch } from '../api/fetchItemsAPI';

export class FetchData extends Component {
    static displayName = FetchData.name;

    constructor(props) {
        super(props);
        this.state = {
            itemsData: [],
            loading: true,
            code: '',
            value: '',
            jsonData: ''
        };
    }

    componentDidMount() {
        this.getItemsData();
    }

    renderTable(itemsData, code, value) {
        return (
            <div>
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
                <div> Filters: </div>
                <div style={displayGridData.flexRow}>
                    <div style={displayGridData.flexRow}>
                        <div style={displayGridData.flexColumn}>
                            <span> Code </span>
                            <input
                                id="code"
                                name="code"
                                type="number"
                                value={code}
                                onChange={e => this.setCodeChange(e.target.value)} >
                            </input>
                        </div>
                        <div style={displayGridData.flexColumn}>
                            <span> Value </span>
                            <input
                                id="value"
                                name="value"
                                type="text"
                                maxLength="1024"
                                value={value}
                                onChange={e => this.setValueChange(e.target.value)} >
                            </input>
                        </div>
                        <div style={displayGridData.flexColumn}>
                            <button onClick={() => this.clearFilters()}>
                                Clear filters
                            </button>
                        </div>
                    </div>
                </div>
                <table className="table table-striped" aria-labelledby="tableLabel">
                    <thead>
                        <tr>
                            <th>Code</th>
                            <th>Value</th>
                        </tr>
                    </thead>
                    <tbody>
                        {itemsData.result.map((item, index) =>
                            <tr key={index}>
                                <td>{item.code}</td>
                                <td>{item.value}</td>
                            </tr>
                        )}
                    </tbody>
                </table>
                <div>Total records: {itemsData.total}</div>
            </div>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderTable(this.state.itemsData, this.state.code, this.state.value);

        return (
            <div>
                <h1 id="tableLabel">Items data</h1>
                {contents}
            </div>
        );
    }

    async getItemsData(code, value) {
        const params = {
            code: code ?? '',
            value: value ?? '',
        };

        const data = await getItemsDataFetch(params);
        this.setState({ itemsData: data, loading: false, code: code, value: value });
    }
    
    setCodeChange(event) {
        this.getItemsData(event, this.state.value);
    }

    setValueChange(event) {
        this.getItemsData(this.state.code, event);
    }

    isValidateInput(event) {
        try{
            JSON.parse(event);
        } catch (e) {
            return false;
        }
        return true;
    }

    clearFilters() {
        this.getItemsData('', '');
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

const displayGridData = {
    flexColumn: {
        padding: "0.5em",
        display: "flex",
        flexDirection: "column",
        justifyContent: "end",
    },
    flexRow: {
        padding: "0.5em",
        display: "flex",
        flexDirection: "row",
        justifyContent: "start",
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
