class App extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            selectedDate: '1999-12-12',
            openDates: []
        }
        this.getDates();
    };
    getDates = () => {
        var httpRequest = new XMLHttpRequest();
        httpRequest.addEventListener('load', (e) => {
            let dates = JSON.parse(e.target.response);
            this.setState({ openDates: dates, selectedDate: dates[dates.length - 1] });
        });
        httpRequest.open('GET', 'api/outdoor/list/');
        httpRequest.send();
    }
    changeDate = (e) => {
        this.setState({ selectedDate: e.target.value });
    }
    render() {
        return (
            <div>
                <Cal
                    onChange={this.changeDate}
                    minDate={this.state.openDates[0]}
                    maxDate={this.state.openDates[this.state.openDates.length - 1]}
                    value={this.state.selectedDate}
                />
                <VideoSelector
                    selectedDate={this.state.selectedDate}
                />
            </div>
        )
    }
}

const Cal = (props) => {
    return (
        <div>
            <input type="date"
                onChange={props.onChange}
                min={props.minDate}
                max={props.maxDate}
                value={props.value}
            />
        </div>
    )
}

class VideoSelector extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            selectedVideo: 'none',
            videos: []
        }
    }

    componentWillReceiveProps = (newProps) => {
        this.getVideos(newProps.selectedDate);
    }

    getVideos = (date) => {
        var httpRequest = new XMLHttpRequest();
        httpRequest.addEventListener('load', (e) => {
            let videos = JSON.parse(e.target.response);
            videos.reverse();
            this.setState({ videos: videos });
        });
        httpRequest.open('GET', 'api/outdoor/list/' + date);
        httpRequest.send();
    }

    playVideo = (videoId) => {
        this.setState({ selectedVideo: videoId });
    }

    render() {

        return (
            <div>
                <Video
                    src={this.state.selectedVideo}
                />
                <div style={{height:'300px', overflowY:'scroll'}}>
                    {this.state.videos.map(vid => <VideoButton
                        key={vid.key}
                        zkey={'api/outdoor/file/' + vid.key}
                        date={vid.date}
                        size={vid.size}
                        clickHandler={this.playVideo}
                    />)}
                </div>
            </div>
        )
    }
}

const VideoButton = (props) => {
    return (
        <div>
            <input type="button"
                onClick={() => props.clickHandler(props.zkey)}
                value={props.date}
            />
        </div>
    )
}

const Video = (props) => {
    if (props.src == 'none') { return null };
    return (
        <div>
            <video width="640" height="480" src={props.src} controls autoPlay></video>
        </div>
    )
}

ReactDOM.render(<App />, document.getElementById('root'));