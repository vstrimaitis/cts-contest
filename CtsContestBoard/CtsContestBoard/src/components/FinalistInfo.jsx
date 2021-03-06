﻿import React from 'react';
import { Image } from 'semantic-ui-react';

const FinalistInfo = ({ image, name, points }) => (
    <div className="podium-step-header">
        <div className="podium-step-header-info">
            <div className="podium-step-header-image">
                <Image src={image ? image : 'img/186382-128.png'} centered={true} shape="circular" />
            </div>
            <h1 className="podium-step-header-info-name">
                {name}
            </h1>
        </div>
        <div className="podium-step-header-info-points">
            <p>
                {points}
            </p>
        </div>
    </div>
)
export default FinalistInfo;