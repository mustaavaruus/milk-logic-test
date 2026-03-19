import styles from './StateContainer.module.css';
import { StateContainerPropsModel } from "../models/state-container-props-model";
import { StateEnum } from '../enums/state-enum';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCheck, faClose, faUniversalAccess, faWarning } from '@fortawesome/free-solid-svg-icons';


const StateContainer: React.FC<StateContainerPropsModel> = (data: StateContainerPropsModel) => {

  const getStateDescriptionText = (state: StateEnum | null) : string => {
    
    if (state == StateEnum.OK) {
      return "Ok";
    } else if (state == StateEnum.Warning) {
      return "Warning";
    } else if (state == StateEnum.Error) {
      return "Error";
    }

    return "";
  }

  const getStateDescriptionStyle = (state: StateEnum) => {
    
    if (state == StateEnum.OK) {
      return <FontAwesomeIcon className={styles.stateVisualWrapperColorGreen} icon={faCheck} />
    } else if (state == StateEnum.Warning) {
      return <FontAwesomeIcon className={styles.stateVisualWrapperColorYellow} icon={faWarning} />
    } else if (state == StateEnum.Error) {
      return <FontAwesomeIcon className={styles.stateVisualWrapperColorRed}  icon={faClose} />
    }
  } 

  /*if (!data.state) {
    return (<div>1</div>);
  }*/
  return (
    <div className={styles.wrapper}>
      <div>
        <span className={styles.stateVisualWrapper}>
          {getStateDescriptionStyle(data.state)}
        </span>
        <span className={styles.stateTextWrapper}>
          {`${getStateDescriptionText(data.state)}`}
        </span>
      </div>
    </div>
  );
}

export default StateContainer;