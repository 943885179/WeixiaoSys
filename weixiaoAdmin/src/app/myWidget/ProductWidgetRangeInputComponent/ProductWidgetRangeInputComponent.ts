import { ControlWidget } from '@delon/form';
import { Component } from '@angular/core';
@Component({
    selector: 'product-widget',
    templateUrl: './ProductWidgetRangeInputComponent.html',
})
export class ProductWidgetRangeInputComponent extends ControlWidget {
    /* 用于注册小部件 KEY 值 */
    static readonly KEY = 'range-input';

    // 价格区间
    from: any;
    to: any;

    getRange = () => {
        return {
            from: this.from,
            to: this.to,
        };
    }

    onChange() {
        const v = this.getRange();
        this.setValue(v);
    }

    // reset 可以更好的解决表单重置过程中所需要的新数据问题
    reset(value: any) {
        const { from, to } = value;
        this.from = from;
        this.to = to;
        this.setValue(value);
    }

}
