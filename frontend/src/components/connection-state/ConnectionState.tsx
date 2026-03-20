import { useEffect, useState } from "react";
import styles from './ConnectionState.module.css';
import { ConnectionPropsModel } from "../../models/connection-props-model";
import { ConnectionEnum } from "../../enums/connection-enum";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faPlane, faPlug, faRefresh, faWarning } from "@fortawesome/free-solid-svg-icons";
import { motion } from "framer-motion";

const ConnectionState = (data: ConnectionPropsModel) => {

    let [connection, setConnection] = useState<ConnectionEnum>(data.state);
    let [attempts, setAttempts] = useState<number>(data.attempts);
    let [attemptsMax, setAttemptsMax] = useState<number>(data.attemptsMax);

    useEffect(() => {
        setConnection(data.state);
        setAttempts(data.attempts);
        setAttemptsMax(data.attemptsMax);
    }, [data]);


    const getConnectionWrapper = () => {

        if (connection == ConnectionEnum.Online) {
            return (<div className={styles.green}><FontAwesomeIcon icon={faPlug} />CONNECTED</div>);
        } else if (connection == ConnectionEnum.Reconnecting) {
            return (<div className={styles.yellow}>
                <motion.div
                    animate={{
                        opacity: [1, 0.5, 1],
                    }}

                    transition={{
                        duration: 1.5,
                        ease: "easeInOut",
                        repeat: Infinity,
                        repeatType: "loop",
                    }}
                >
                    <FontAwesomeIcon icon={faRefresh} />
                    Reconnecting, (попытка {attempts} из {attemptsMax})
                </motion.div>
            </div>);
        } else if (connection == ConnectionEnum.Offline) {
            return (<div className={styles.red}><FontAwesomeIcon icon={faWarning} />Offline (данные не обновляются)</div>);
        }
        else {
            return (<div><FontAwesomeIcon icon={faWarning} />Offline</div>);
        }
    }
    return (getConnectionWrapper());
}

export default ConnectionState;