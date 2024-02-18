import discuss_Vi from '../modules/discuss/i18n/vi-VN';
import discuss_EN from '../modules/discuss/i18n/en-US';
import core_VN from '../modules/core/i18n/vi-VN';
import core_US from '../modules/core/i18n/en-US';

export default {
  'en-US': {
    ...discuss_EN,
    ...core_US,
  },
  'vi-VN': {
    ...discuss_Vi,
    ...core_VN,
  },
};
