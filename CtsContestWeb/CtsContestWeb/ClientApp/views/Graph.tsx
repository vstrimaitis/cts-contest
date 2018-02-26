﻿enum NodeType { Q , A }

class GraphNode {
    id: number;
    text: string;
    edges: Edge[];
    type: NodeType;

    constructor(id: number, text: string, type: NodeType) {
        this.id = id;
        this.text = text;
        this.type = type;
        this.edges = [];
    }
}

class Edge {
    id: number;
    text: string;

    constructor(id: number, text: string) {
        this.id = id;
        this.text = text;
    }
}

class Graph {
    nodes: GraphNode[];
    curr: GraphNode;
    constructor() {
        this.nodes = [];
    }

    addQuestion(idx: number, question: string) {
        var node = new GraphNode(idx, question, NodeType.Q);
        this.nodes.push(node);
    }

    addAnswer(idx: number, answer: string) {
        var node = new GraphNode(idx, answer, NodeType.A);
        this.nodes.push(node);
    }

    addEdge(from: number, to: number, text: string) {
        var fromNode = this.nodes.filter(x => x.id === from)[0];
        fromNode.edges.push({
            id: to,
            text: text
        });
    }

    setCurr(id: number) {
        this.curr = this.nodes.filter(x => x.id === id)[0];
    }

    answer(ansText: string) {
        var idTo = this.curr.edges.filter(x => x.text == ansText)[0].id;
        this.setCurr(idTo);
    }
}

const g = new Graph();
g.addQuestion(0, 'Do you like new career challenges & opportunities?');
g.addQuestion(1, 'Does your love language includes .NET/Java/Python/CSS & etc.?');
g.addQuestion(2, 'Do you feel experienced enough to dive into international IT ocean?');
g.addQuestion(3, 'If you would be a musician, what you prefer more: ');
g.addAnswer(4, 'Don\'t worry, You can grow your knowledge together with us. Find Marija, tell code "1001" and You will get a gift.');
g.addAnswer(5, 'Congratulations! You are meant to be a front end developer. Find Marija, tell the code "FR10" and You will get a gift.');
g.addAnswer(6, 'Congratulations! You are meant to be a back end developer. Find Marija, tell the code "BC11" and You will get a gift.');
g.addAnswer(7, 'Congratulations! You are meant to be a test analyst. Find Marija, tell the code "TS01" and You will get a gift.');
g.addAnswer(8, 'Try again :)');
g.addQuestion(9, 'Can You give positive answer to this question: "Snakker du dansk, norsh eller svensk"?');
g.addQuestion(10, 'Do You feel strong enough to use Danish/Norwegian or Swedish at work?');
g.addQuestion(11, 'When you are planning trip with friends, what role do You take:');
g.addAnswer(12, 'Congratulations! You are meant to work in finance field. Find Ieva, tell the code "FIN1" and You will get a gift.');
g.addAnswer(13, 'Congratulations! You are meant to work with business process managment. Find Ieva, tell the code "BPS2" and You will get a gift.');
g.addQuestion(14, 'Would You like to become guru in one of the Scandinavian language?');
g.addAnswer(15, 'Congratulations! You could fulfill this dream by joining our language courses and becoming Jr. Finance Specialist. Find Ieva, tell the code "April16" and You will get a gift.');
g.addQuestion(16, 'When you are planning trip with friends, what role do You take:');
g.addAnswer(17, 'Congratulations! You are meant to work with business process managment. Find Ieva, tell the code "PM01" and You will get a gift.');
g.addAnswer(18, 'Congratulations! You are meant to work with analytics. Find Marija, tell the code "BA02" and You will get a gift.');

g.addEdge(0, 1, "YES");
g.addEdge(0, 8, "NO");
g.addEdge(1, 2, "YES");
g.addEdge(1, 9, "NO");
g.addEdge(2, 3, "YES");
g.addEdge(2, 4, "NO");
g.addEdge(3, 5, "FRONT STAGE");
g.addEdge(3, 6, "BACK STAGE");
g.addEdge(3, 7, "SOUND QUALITY TESTER");
g.addEdge(9, 10, "YES");
g.addEdge(10, 11, "YES");
g.addEdge(11, 12, "BUDGET MASTER");
g.addEdge(11, 13, "ACTIVITIES GURU");
g.addEdge(9, 14, "NO");
g.addEdge(10, 14, "NO");
g.addEdge(14, 15, "YES");
g.addEdge(14, 16, "NO");
g.addEdge(16, 18, "ACTIVITIES GURU");
g.addEdge(16, 17, "NAVIGATION OWNER");

g.setCurr(0);
export default g;