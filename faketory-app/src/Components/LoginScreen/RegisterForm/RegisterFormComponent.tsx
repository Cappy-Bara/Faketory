import { Form,Button } from "react-bootstrap";
import "./styles.css";


const RegisterFormComponent = ({setIsRegistered}:any) => {

    const handleLogin = () => {
        setIsRegistered(true);
    }


    return (
        <>
        <h1>Register</h1>
        <Form>
            <Form.Group className="mb-3" controlId="formBasicEmail">
                <Form.Label>Email address</Form.Label>
                <Form.Control type="email" placeholder="Enter email" />
            </Form.Group>

            <Form.Group className="mb-3" controlId="formBasicPassword">
                <Form.Label>Password</Form.Label>
                <Form.Control type="password" placeholder="Password" />
            </Form.Group>

            <Form.Group className="mb-3" controlId="formBasicPassword">
                <Form.Label>Repeat Password</Form.Label>
                <Form.Control type="repeat-password" placeholder="Repeat Password" />
            </Form.Group>

            <Button variant="primary" type="submit">
                Submit
            </Button>
            <span className="float-end pt-2 normal-text">
                Already have an account? <span className="text-button" onClick={handleLogin}> Login!</span>
            </span>
        </Form>
    </>
    );
}

export default RegisterFormComponent;