import { useEffect, useState } from "react";
import styles from './TableCellCustom.module.css';
import { TableCellPropsModel } from "../../models/table-cell-props-model";
import { motion } from "framer-motion";


const TableCellCustom = (data: TableCellPropsModel) => {

    let [value, setValue] = useState(data.value);
    let [oldValue, setOldValue] = useState(data.value);
    let [dataVal, setDataVal] = useState(data.data);

    const getColor = () => {

        if (value > oldValue) {
            return ["#ffffff", "#9cf379", "#ffffff"];
        } else if (value < oldValue) {
            return ["#ffffff", "#79c6f3", "#ffffff"];
        }

        return "#ffffff";
    }

    const getScale = () => {

        if (value > oldValue) {
            return [1, 1.05, 1]
        } else if (value < oldValue) {
            return [1, 0.95, 1]
        }

        return 1;
    }

    useEffect(() => {
        setOldValue(value);
        setValue(data.value);
        setDataVal(data.data);
    },
        [data]);

    return (
        <div className={styles.wrapper}>
            <motion.div
                initial={{ color: "#ffffff" }}
                animate={{ 
                    color: value != oldValue ? getColor() : getColor(), 
                    scale: value != oldValue ? getScale() : 1,
                    animationIterationCount: 1,
                    animationDirection: "alternate"
                }}
                transition={
                    { 
                        duration: 2,
                        ease: "easeInOut"
                    }
                }
            >
                {dataVal}
            </motion.div>
        </div>
    );

}

export default TableCellCustom;