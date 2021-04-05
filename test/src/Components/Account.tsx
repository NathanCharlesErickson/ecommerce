import {useState} from 'react'

const Account = () => {
    const [counter, setCounter] = useState<number>(5);

    function clickHandler() {
        setCounter(counter + 1);
    }

    return (
        <div className='wrapper'>
            <p>Account Pages</p>
            <button className="button" onClick={clickHandler}> {counter}</button>
        </div>
    )
}

export default Account;