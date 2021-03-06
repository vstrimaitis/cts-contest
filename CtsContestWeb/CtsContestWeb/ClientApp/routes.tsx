import * as React from 'react';
import { Route } from 'react-router-dom';
import { Layout } from './components/Layout';
import { FetchData } from './components/FetchData';
import { Tasks } from './views/Tasks';
import { About } from './views/About';
import { Prizes } from './views/Prizes';
import { Shop } from './views/Shop';
import { Quiz } from './views/Quiz';
import { TaskComponent } from './views/Task';
import { Duel } from './views/Duel';
import { UserInfo } from './components/models/UserInfo';
import { Leaderboard } from './views/Leaderboard';
import PrivacyPolicy from './views/PrivacyPolicy'

export class Routes extends React.Component<any, any> {
    constructor(props:any) {
        super(props);

        this.state = {
            //TODO: should be changed from any to defined class
            userInfo: {
                isLoggedIn: false,
                name: '',
                balance: 0,
                todaysBalance: 0,
                totalBalance: 0
            }
        }

        this.incrementBalance = this.incrementBalance.bind(this);
        this.decrementBalance = this.decrementBalance.bind(this);
    }

    componentDidMount() {
        fetch('api/User', {
            credentials: 'include'
        })
        .then(response => response.json() as Promise<any>)
        .then(data => {
            this.setState({userInfo: data});
        });
    }

    incrementBalance(value:number){
        const userInfo = this.state.userInfo;
        userInfo.todaysBalance = userInfo.todaysBalance + value;
        userInfo.totalBalance = userInfo.totalBalance + value;
        this.setState({userInfo});
    }

    decrementBalance(value:number){
        const userInfo = this.state.userInfo;
        userInfo.todaysBalance = userInfo.todaysBalance - value;
        userInfo.totalBalance = userInfo.totalBalance - value;
        this.setState({userInfo});
    }

    render() {
        return (
            <Layout userInfo={this.state.userInfo}>
                <Route exact path='/' component={About} />
                <Route exact path='/tasks' render={(props) => <Tasks {...props} userInfo={this.state.userInfo}/>} /> 
                {/* <Route exact path='/duel' render={(props: any) => <Duel {...props} userInfo={this.state.userInfo} onIncrementBalance={this.incrementBalance} />} /> */}
                <Route path='/shop' render={(props:any) => <Shop {...props} userInfo={this.state.userInfo} onDecrementBalance={this.decrementBalance}/>} />
                <Route path='/prizes' render={(props:any) => <Prizes {...props} userInfo={this.state.userInfo} onDecrementBalance={this.decrementBalance}/>} />
                <Route path="/tasks/:id" render={(props: any) => <TaskComponent {...props} userInfo={this.state.userInfo} onIncrementBalance={this.incrementBalance} />} />
                <Route exact path='/quiz' component={Quiz}/>
                <Route exact path="/leaderboard" component={Leaderboard} />
                <Route path='/privacyPolicy' render={() => <PrivacyPolicy/>} />
            </Layout>
        )
    }
}
