﻿import React from 'react';
import { Grid } from 'semantic-ui-react';
import ReactCountdownClock from 'react-countdown-clock';

import UserCard from './UserCard.jsx';
import WeekPrizeCard from './WeekPrizeCard.jsx';

export default class SpecialPrizeBoard extends React.Component {
    render() {
        console.log(this.props.data)
        return (
            <TwoPrizeBoard data={this.props.data} />
        );
    }

}

const TwoPrizeBoard = ({ data }) => (
    <Grid id="special-prize-board">
        <Grid.Row columns={3} className="first-week-prize-row">
            <Grid.Column width={5} className="full-space">
                <Grid.Row className="first-prize-card-row">
                    <WeekPrizeCard name="IPhone X" picture="http://drop.ndtv.com/TECH/product_database/images/913201720152AM_635_iphone_x.jpeg" />
                </Grid.Row>
                <Grid.Row className="second-prize-card-row">
                    <WeekPrizeCard name="IPhone X" picture="http://drop.ndtv.com/TECH/product_database/images/913201720152AM_635_iphone_x.jpeg" />
                </Grid.Row>
            </Grid.Column>
            <Grid.Column width={7}>
                
            </Grid.Column>
            <Grid.Column width={4}>
                <ReactCountdownClock seconds={86400}
                    color="#000"
                    alpha={0.9}
                    size={300}
                    onComplete={() => { }} />
            </Grid.Column>
        </Grid.Row>
        <Grid.Row columns={2} className="second-week-prize-row">
            <Grid.Column width={5} className="white-space">
            </Grid.Column>
            <Grid.Column width={11}>
                <Podium first={data[0]} second={data[1]} third={data[2]} />
            </Grid.Column>
        </Grid.Row>
    </Grid>
)

const OnePrizeBoard = ({ data }) => (
    <Grid id="special-prize-board">
        <Grid.Row columns={2} className="first-week-prize-row">
            <Grid.Column width={12}>
               
            </Grid.Column>
            <Grid.Column width={4}>
                <ReactCountdownClock seconds={86400}
                    color="#000"
                    alpha={0.9}
                    size={300}
                    onComplete={() => { }} />
            </Grid.Column>
        </Grid.Row>
        <Grid.Row columns={2} className="second-week-prize-row">
            <Grid.Column width={5}>
                <WeekPrizeCard name="IPhone X" picture="http://drop.ndtv.com/TECH/product_database/images/913201720152AM_635_iphone_x.jpeg" />
            </Grid.Column>
            <Grid.Column width={11}>
                <Podium first={data[0]} second={data[1]} third={data[2]} />
            </Grid.Column>
        </Grid.Row>
    </Grid>
)

const Podium = ({ first, second, third}) => (
    <Grid id="podiumDiv">
        <Grid.Column width={1} />
        <Grid.Column width={14}>
            <Grid className="podium">
                <Grid.Column width={5} className="secondPlace podium-step">
                    <UserCard username={second.username} points={second.points} picture={second.picture} />
                </Grid.Column>
                <Grid.Column width={6} className="firstPlace podium-step">
                    <UserCard username={first.username} points={first.points} picture={first.picture}/>
                </Grid.Column>
                <Grid.Column width={5} className="thirdPlace podium-step">
                    <UserCard username={third.username} points={third.points} picture={third.picture} />
                </Grid.Column>
            </Grid>
        </Grid.Column>
        <Grid.Column width={1} />
    </Grid>
)