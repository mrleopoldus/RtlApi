import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';

interface FetchShowDataState {
    shows: Show[];
    loading: boolean;
    totalPages: number;
    page: number;
    currentpage: number;
}


export class FetchShowData extends React.Component<RouteComponentProps<{}>, FetchShowDataState> {
    constructor() {
        super();
        this.state = { shows: [], loading: true, totalPages: 0, page: 0, currentpage: 0 };
        this.fetchIt();
    }

    private fetchIt() {
        fetch('home/Shows?page=' + this.state.currentpage)
            .then(response => response.json() as Promise<QueryResult>)
            .then(data => {

                this.setState({ shows: data.shows, loading: false });
                this.setState({ totalPages: data.totalPages, loading: false });
                this.setState({ page: data.page, loading: false });

            })

    }

    public render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderShowsTable(this.state.shows);

        return <div>
            <h1>Shows</h1>
            <p>This component demonstrates fetching data from the server.</p>
            {contents}
        </div>;
    }

    private renderShowsTable(shows: Show[]) {
        return <div>
            <h1>Huidige pagina: {this.state.currentpage}</h1>
            <h2>{this.renderPagination()}</h2>
            <table className='table'>
                <thead>
                    <tr>
                        <th>Naam</th>
                        <th>Cast</th>

                    </tr>
                </thead>
                <tbody>
                    {shows.map(show =>
                        <tr key={show.name}>
                            <td>{show.name}</td>
                            <tr>{this.renderCast(show.cast)}</tr>
                        </tr>
                    )}
                </tbody>
            </table>
        </div>;
    }

    private renderCast(cast: CastMember[]) {
        if (cast != null) {
            return cast.map(cast =>
                <div>
                    <td width="200">{cast.name}</td> <td>{cast.birthDay}</td>
                </div>
            )
        }
    }

    private renderPagination() {
        var rows: JSX.Element[] = [];
        //var rows = any[];
        for (let i = 0; i < this.state.totalPages; i++) {
            rows.push(<a onClick={() => this.updatePage(i)}>{i} </a>);
        }
        return rows;

    }

    private updatePage(newpage: number) {
        this.setState({ currentpage: newpage }, function (this) {
            this.fetchIt();
        });

    }

}

interface QueryResult {
    shows: Show[];
    totalPages: number;
    page: number;
}

interface Show {
    name: string;
    cast: CastMember[];
}

interface CastMember {
    name: string;
    birthDay: Date | null;
}
