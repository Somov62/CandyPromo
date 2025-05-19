import { Accordion, AccordionTab } from 'primereact/accordion';

import './FAQ.css'
import {questions} from "@/App/Pages/Main/Components/FAQ/questions.js";

const Faq = () => {
    return (
        <div className="faq" id="questions">
            <div>
                <h1>вопрос-ответ</h1>
                <Accordion>{getQuestions()}</Accordion>
            </div>
        </div>
    );
};

function getQuestions() {
    return questions.map((tab) => {
        return (
            <AccordionTab key={tab.question} header={tab.question} disabled={tab.disabled}>
                <p className="m-0" style={{whiteSpace: 'pre-line'}}>
                    {tab.answer}
                </p>
            </AccordionTab>
        );
    });
}

export default Faq;