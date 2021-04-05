export interface CardInfo {
    
    title: string;
    icon?: string;
    disabled?: boolean;
    content: {label: string, value: any}[]
}