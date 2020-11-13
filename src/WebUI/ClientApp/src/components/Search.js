import React, { Component } from 'react';

export class Search extends Component {
    static displayName = Search.name;

    constructor(props) {
        super(props);
        this.state = {
            products: [],
            errors: [],
            errorText: '',
            loadStatus: 'init',
            searchText: ''
        };
        this.handleChange = this.handleChange.bind(this);
    }

    handleChange = (e) => {
        this.setState({ [e.target.name]: e.target.value });
    }

    render() {
        return (
            <>
                <h2 id="tabelLabel" >Product List</h2>
                <label> Enter at least 2 characters for Search : <input type="text" name="searchText" value={this.state.searchText} onChange={this.handleChange} /> </label>
                <input type="button" value="Search" onClick={() => { this.getSerachData(this.state.searchText) }} />
                <table className='table table-striped' aria-labelledby="tabelLabel">
                    <thead>
                        <tr>
                            <th>Category</th>
                            <th>Product</th>
                            <th>Description</th>
                            <th>Price</th>
                        </tr>
                    </thead>
                    <tbody>
                        {this.state.loadStatus == 'Loaded' ?
                            this.state.products.map(product =>
                                <tr key={product.productID}>
                                    <td>{product.categoryName}</td>
                                    <td>{product.productName}</td>
                                    <td>{product.description}</td>
                                    <td>{product.unitPrice}</td>
                                </tr>
                            ) : this.state.loadStatus == 'Loading' ?
                                <p><em>Loading...</em></p>
                              : this.state.loadStatus == 'NotFound' ?
                                <p>Result not found !</p>
                              : this.state.loadStatus == 'Validation' ?
                                <ul>
                                    {this.state.errors.map(error =>
                                        <li>{error}</li>
                                    )}
                                </ul>
                              : this.state.loadStatus == 'ERROR' ?
                                <p>ERROR: {this.state.errorText}</p>
                              : null
                        }
                    </tbody>
                </table>
            </>
        );
    }

    async getSerachData(searchText) {
        if (searchText.length == 0) {
            this.setState({ errors: ['Search text  should not be empty'], loadStatus: 'Validation' });
            return;
        }
        this.setState({ loadStatus: 'Loading' });
        const response = await fetch('api/products/search/' + searchText);
        const data = await response.json();
        console.log('data.errors : ', data.errors);
        console.log('data : ', data);
        if (data.length > 0)
            this.setState({ products: data, loadStatus: 'Loaded' });
        else if (data.errors != undefined) {
            if (data.errors.SearchText != undefined)
                this.setState({ errors: data.errors.SearchText, loadStatus: 'Validation' });
            else
                this.setState({ errorText: data.title, loadStatus: 'Error' });
        }
        else
            this.setState({ products: [], loadStatus: 'NotFound' });
    }
}
