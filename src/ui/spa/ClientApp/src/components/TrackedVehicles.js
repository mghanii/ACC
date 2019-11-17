import React, { Component } from 'react';

export class TrackedVehicles extends Component {
    static displayName = TrackedVehicles.name;
    static gatewayUrl = "http://localhost:5006"; // TODO: read it from settings

    constructor(props) {
        super(props);
        this.state = {
            trackedVehicles: [],
            customers: [],
            customerId: "",
            status: "",
            loading: true
        };

        this.handleCustomerChange = this.handleCustomerChange.bind(this);
        this.handleStatusChange = this.handleStatusChange.bind(this);
    }

    componentDidMount() {
        this.populateCustomersData();
        this.populateVehiclesData();
    }

    handleCustomerChange(event) {
        this.setState({ customerId: event.target.value });
    }

    handleStatusChange(event) {
        this.setState({ status: event.target.value });
    }

    renderVehiclesTable(vehicles) {
        return (
            <div>
                <div className="row">
                    <div className="col-md-1">Customer</div>
                    <div className="col-md-3">
                        {this.renderCustomersSelect(this.state.customers)}
                    </div>
                    <div className="col-md-1">Status</div>
                    <div className="col-md-3">
                        <select className="form-control" value={this.state.status} onChange={this.handleStatusChange}>
                            <option value="">All</option>
                            <option value="Connected">Connected</option>
                            <option value="Disconnected">Disonnected</option>
                        </select>
                    </div>
                    <div className="col-md-4">
                        <button type="button" className="btn btn-danger" value="" onClick={() => this.populateVehiclesData()} >Search</button>
                    </div>
                </div>
                <br />
                <br />
                <div className="row">
                    <table className='table table-striped' aria-labelledby="tabelLabel">
                        <thead>
                            <tr>
                                <th>Vehicle Id</th>
                                <th>Reg Nr. </th>
                                <th>Status </th>
                                <th>Customer</th>
                                <th>Customer Address</th>
                            </tr>
                        </thead>
                        <tbody>
                            {vehicles.map(vehicle =>
                                <tr key={vehicle.id}>
                                    <td>{vehicle.id}</td>
                                    <td>{vehicle.regNr}</td>
                                    <td>{vehicle.status}</td>
                                    <td>{vehicle.customerName}</td>
                                    <td>{vehicle.customerAddress}</td>
                                </tr>
                            )}
                        </tbody>
                    </table>
                </div>
            </div>
        );
    }

    renderCustomersSelect(customers) {
        return (
            <select className="form-control" value={this.state.customerId} onChange={this.handleCustomerChange}>
                <option value="">All</option>
                {customers.map(c => <option value={c.id}>{c.name}</option>)}
            </select>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderVehiclesTable(this.state.trackedVehicles);

        return (
            <div>
                <h1 id="tabelLabel" >Tracked Vehicles</h1>
                <br />
                {contents}
            </div>
        );
    }

    async populateVehiclesData() {
        const response = await fetch(`${TrackedVehicles.gatewayUrl}/tracking/vehicles?customerId=${this.state.customerId}&status=${this.state.status}`);
        const data = await response.json();
        this.setState({ trackedVehicles: data, loading: false });
    }

    async populateCustomersData() {
        const response = await fetch(`${TrackedVehicles.gatewayUrl}/customers`);
        const data = await response.json();
        this.setState({ customers: data, loading: false });
    }
}