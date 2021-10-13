import { ToggleButton } from "react-bootstrap";

const MainTabComponent = ({ autoTimestamp, setAutoTimestamp }: any) => {
    return (
        <>
            {/* <Button
                        className="my-3 mx-1 col-5"
                        variant="primary"
                        onClick={handleTimestampButton}
                    >
                        Timestamp
                    </Button>

                    <Button
                        className="my-3 mx-1 col-5"
                        variant="primary"
                        onClick={handleGetStaticElementsButton}
                    >
                        Refresh Static Elements
                    </Button> */}

            {<ToggleButton
                className="mb-2"
                id="toggle-check"
                type="checkbox"
                variant="outline-primary"
                checked={autoTimestamp}
                value="0"
                onChange={(e) => {
                    console.log(e.currentTarget.checked)
                    setAutoTimestamp(e.currentTarget.checked);
                }}
            >
                Auto Timestamp
            </ToggleButton>}
        </>
    )
}

export default MainTabComponent;