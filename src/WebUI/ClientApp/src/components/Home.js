import React, { Component } from 'react';

export class Home extends Component {
    static displayName = Home.name;
    constructor(props) {
        super(props);
        this.state = { cars: [], isloaded: false };
    }

    componentDidMount() {
        this.populateWeatherData();
    }

    async populateWeatherData() {
        const response = await fetch('api/cars');
        const data = await response.json();
        this.setState({ cars: data, isloaded: true });
    }

    render() {
        return (
            <>
                <header>
                    <div className="content-wrapper">
                        <div className="float-left">
                            <p className="site-title">
                                <a href="/" id="A2">
                                    <img id="Logo" src="Assets/logo.jpg" style={{ borderStyle: 'None' }} /></a>
                            </p>
                        </div>
                    </div>
                </header>
                <div id="body">
                    <section className="featured">
                        <div className="content-wrapper">
                            <hgroup className="title">
                                <h1>Products</h1>
                            </hgroup>

                            <section className="featured">
                                <div style={{ marginLeft: '2em' }}>
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td style={{ width: '30%', verticalAlign: 'top' }}>
                                                    <hgroup className="title">
                                                        <h1>Wingtip Toys</h1>
                                                        <h2>Wingtip Toys can help you find the perfect gift</h2>
                                                    </hgroup>
                                                    <p>
                                                        We're all about transportation toys. You can order
                                                        any of our toys today. Each toy listing has detailed
                                                        information to help you choose the right toy.
                                        </p>
                                                </td>
                                                <td>
                                                    {this.state.isloaded ?
                                                        <table>
                                                            <tbody>
                                                                {this.state.cars.map(car =>
                                                                    <tr key={car.productID}>
                                                                        <td>
                                                                            <img border="1" height="75" src={'Assets/' + car.imagePath} width="100" />
                                                                        </td>
                                                                        <td>{car.productName}<br />
                                                                            <span className="ProductPrice">
                                                                                <strong>Price:</strong> ${car.unitPrice}</span>
                                                                        </td>
                                                                    </tr>)
                                                                }
                                                            </tbody>
                                                        </table>
                                                        :
                                                        <p><em>Loading...</em></p>
                                                    }
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>

                                </div>
                            </section>
                        </div>
                    </section>

                    <section className="content-wrapper main-content clear-fix"></section>
                </div>

                <footer>
                    <div className="content-wrapper">
                        <div className="float-left">
                            <p>OrderDynamics Coding Exercise</p>
                        </div>
                    </div>
                </footer>      </>
        );
    }
}
