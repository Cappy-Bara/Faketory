import { Button } from "react-bootstrap";
import { Modal } from "react-bootstrap";



const ModifyConveyorModal = ({show,onHide,conveyor}:any) => {
    return (
        <Modal
            show={show}
            onHide={onHide}
            size="sm"
            aria-labelledby="contained-modal-title-vcenter"
            centered
        >
            <Modal.Header closeButton>
                <Modal.Title id="contained-modal-title-vcenter">
                    Modify Conveyor
                </Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <p>
                    {conveyor.id}
                </p>
            </Modal.Body>
            <Modal.Footer>
                <Button onClick={onHide}>Update</Button>
                <Button onClick={onHide}>Delete</Button>
                <Button onClick={onHide}>Close</Button>
            </Modal.Footer>
        </Modal>
    )
}

export default ModifyConveyorModal;