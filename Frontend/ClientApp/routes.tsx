import * as React from 'react';
import { Route } from 'react-router-dom';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchShowData } from './components/Shows';

export const routes = <Layout>
    <Route exact path='/' component={Home} />
    <Route path='/shows' component={FetchShowData} />
</Layout>;
