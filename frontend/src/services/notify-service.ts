import { format } from "date-fns/format";
import { toast } from "react-toastify";

const NotifyService = {

    notifyInfo(text: string): void {
        toast.info(text, {
            position: "bottom-left",
        });
    },

    notifyError(text: string): void {
        toast.error(text, {
            position: "bottom-left",
        });
    },

    notifyWarning(text: string): void {
        toast.warning(text, {
            position: "bottom-left",
        });
    },

};

export default NotifyService;