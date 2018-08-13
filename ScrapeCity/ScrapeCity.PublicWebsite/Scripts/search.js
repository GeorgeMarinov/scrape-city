'use strict';
/* global instantsearch */
//templates
let hrefMonitorDetails = ` href="${window.location.origin}/Monitor/Details/{{Id}}"`;
let hitTemplate = `
    <div class="hit">
        <div class="hit-image">
            <a ${hrefMonitorDetails} ><img class="img-fluid" src="${viewData.ImageDomain}/Monitor/{{Thumbnail}}"></a>
        </div>
        <div class="product-desc-wrapper">
            <div class="product-name">
                <a ${hrefMonitorDetails}>{{Brand.BrandName}}-{{Model}}</a>
            </div>
        </div>
    </div>`;

let noResultsTemplate = '<div class="text-center">No results found matching <strong>{{query}}</strong>.</div>';

let menuTemplate = '<a href="javascript:void(0);" class="facet-item {{#isRefined}}active{{/isRefined}}"><span class="facet-name"><i class="fa fa-angle-right"></i> {{name}}</span class="facet-name"></a>';

let facetTemplateRefinementListCheckBox =`
    <a href="javascript:void(0);" class="">
        <input type="checkbox" class="{{cssClasses.checkbox}} m-2" value="{{label}}" {{#isRefined}}checked{{/isRefined}} />{{label}}
        <span class="facet-count">({{count}})</span>
    </a>`;

let facetTemplateCheckbox = `
    <a href="javascript:void(0);" class="facet-item">
        <input type="checkbox" class="{{cssClasses.checkbox}}" value="{{name}}" {{#isRefined}}checked{{/isRefined}} />{{name}}
        <span class="facet-count">({{count}})</span>
    </a>`;

let facetTemplateColors =
    '<a href="javascript:void(0);" data-facet-value="{{name}}" class="facet-color {{#isRefined}}checked{{/isRefined}}"></a>';
//set serach params
let search = instantsearch({
  appId: viewData.AlgoliaAppId,
  apiKey: viewData.AlgoliaApiKey,
  indexName: viewData.AlgoliaMonitorsIndex
});
//serach box
search.addWidget(
  instantsearch.widgets.searchBox({
    container: '#searchBox',
    placeholder: 'Search for a product',
    magnifier: false,
    reset: false,
  })
);
//main window
//load animation
search.on('render', function () {
    $('.product-picture img').addClass('transparent');
    $('.product-picture img').one('load', function () {
        $(this).removeClass('transparent');
    }).each(function () {
        if (this.complete) $(this).load();
    });
});
////hit count
//search.addWidget(
//  instantsearch.widgets.stats({
//    container: '#stats'
//  })
//);
//display hits
search.addWidget(
    instantsearch.widgets.infiniteHits({
    container: '#hits',
    templates: {
      empty: noResultsTemplate,
      item: hitTemplate
    },
    escapeHits: true,
    })
);
////pagination
//search.addWidget(
//  instantsearch.widgets.pagination({
//    container: '#pagination',
//    cssClasses: {
//      active: 'active'
//    },
//    labels: {
//      previous: '<i class="fa fa-angle-left fa-2x"></i> Previous page',
//      next: 'Next page <i class="fa fa-angle-right fa-2x"></i>'
//    },
//    showFirstLast: false
//  })
//);
//aside
//list of filters
search.addWidget(
  instantsearch.widgets.currentRefinedValues({
    container: '#currentRefinedValues',
    clearAll: 'after',
    transformData: function(data) {
      return data;
    },
    templates: {
      item: function(item) {
        let html = '';
        if (item.type === 'disjunctive' && item.name === 'false') {
          html = '';
        } else if (item.count > 0) {
            html = `<span class="ais-current-refined-values--attribute-name">${item.attributeName}</span>:  <span class="ais-current-refined-values--name">${item.name}</span> <span class="ais-current- refined-values--count">(${item.count})</span> <span class="glyphicon glyphicon-remove"></span>`;
        } else {
            html = `<span class="ais-current-refined-values--attribute-name">${item.attributeName}</span>:  <span class="ais-current-refined-values--name">${item.name}</span> <span class="glyphicon glyphicon-remove"></span>`;
        }
        return html;
        },
      clearAll: '<i class="fa fa-eraser rounded-0"></i> Clear all filters'
    },
    cssClasses: {
        item: 'mt-1',
        clearAll: 'btn btn-block btn-primary rounded-0 mt-1'
    }
  })
);
//brand filter
search.addWidget(
  instantsearch.widgets.refinementList({
    container: '#brandRefinementList',
    attributeName: 'Brand.BrandName',
    sortBy: ['isRefined', 'count:desc', 'name:asc'],
    limit: 4,
    showMore: true,
    operator: 'or',
    templates: {
      item: facetTemplateRefinementListCheckBox,
      header: '<button class="btn btn-block btn-primary rounded-0 rounded-0">Manufacturer</button>'
    },
    //searchForFacetValues: {
    //  isAlwaysActive: true,
    //  placeholder: 'Search for brands',
    //  templates: {
    //    noResults: '<div class="sffv_no-results">No matching brands.</div>'
    //  }
    //},
    collapsible: true,
  })
);
//screen size filter
search.addWidget(
  instantsearch.widgets.rangeSlider({
    container: '#screenSizeSlider',
    attributeName: 'ScreenSize',
    min: viewData.ScreenSizeMin,
    max: viewData.ScreenSizeMax,
    templates: {
        header: '<button class="btn btn-block btn-primary rounded-0">Screen size</button>'
    },
    tooltips: {
      format: function(rawValue) {
        return Math.round(rawValue).toLocaleString();
      }
    },
    collapsible: true,
  })
);

//panel type filter
search.addWidget(
    instantsearch.widgets.refinementList({
        container: '#panelTypeRefinementList',
        attributeName: 'PanelType',
        sortBy: ['isRefined', 'count:desc', 'name:asc'],
        limit: 4,
        showMore: true,
        operator: 'or',
        templates: {
            item: facetTemplateRefinementListCheckBox,
            header: '<button class="btn btn-block btn-primary rounded-0">Panel type</button>'
        },
        //searchForFacetValues: {
        //    isAlwaysActive: true,
        //    placeholder: 'Search for panel types',
        //    templates: {
        //        noResults: '<div class="sffv_no-results">No matching types.</div>'
        //    }
        //},
        collapsible: true,
    })
);

//backlight type refinement list
search.addWidget(
    instantsearch.widgets.refinementList({
        container: '#backlightTypeRefinementList',
        attributeName: 'BacklightType',
        sortBy: ['isRefined', 'count:desc', 'name:asc'],
        limit: 4,
        showMore: true,
        operator: 'or',
        templates: {
            item: facetTemplateRefinementListCheckBox,
            header: '<button class="btn btn-block btn-primary rounded-0">Backlight</button>'
        },
        //searchForFacetValues: {
        //    isAlwaysActive: true,
        //    placeholder: 'Search for backlight types',
        //    templates: {
        //        noResults: '<div class="sffv_no-results">No matching types.</div>'
        //    }
        //},
        collapsible: true,
    })
);

//resolution filter
search.addWidget(
  instantsearch.widgets.refinementList({
    container: '#horizontalResolutionRefinementList',
    attributeName: 'MaxHorizontalPixels',
    sortBy: ['isRefined', 'count:desc', 'name:asc'],
    limit: 4,
    showMore: true,
    operator: 'or',
    templates: {
      item: facetTemplateRefinementListCheckBox,
      header: '<button class="btn btn-block btn-primary rounded-0">Horizontal pixels</button>'
    },
    //searchForFacetValues: {
    //  isAlwaysActive: true,
    //  placeholder: 'Search for resolution',
    //  templates: {
    //    noResults: '<div class="sffv_no-results">No matching resolution.</div>'
    //  }
    //},
    collapsible: true,
  })
);

search.addWidget(
  instantsearch.widgets.refinementList({
    container: '#verticalResolutionRefinementList',
    attributeName: 'MaxVerticalPixels',
    sortBy: ['isRefined', 'count:desc', 'name:asc'],
    limit: 4,
    showMore: true,
    operator: 'or',
    templates: {
      item: facetTemplateRefinementListCheckBox,
      header: '<button class="btn btn-block btn-primary rounded-0">Vertical pixels</button>'
    },
    //searchForFacetValues: {
    //  isAlwaysActive: true,
    //  placeholder: 'Search for resolution',
    //  templates: {
    //    noResults: '<div class="sffv_no-results">No matching resolution.</div>'
    //  }
    //},
    collapsible: true,
  })
);

//viewing angles
search.addWidget(
    instantsearch.widgets.refinementList({
        container: '#horizontalViewingAngleRefinementList',
        attributeName: 'HorizontalViewingAngle',
        sortBy: ['isRefined', 'count:desc', 'name:asc'],
        limit: 4,
        showMore: true,
        operator: 'or',
        templates: {
            item: facetTemplateRefinementListCheckBox,
            header: '<button class="btn btn-block btn-primary rounded-0">Horizontal viewing angle</button>'
        },
        //searchForFacetValues: {
        //    isAlwaysActive: true,
        //    placeholder: 'Search for viewing angle',
        //    templates: {
        //        noResults: '<div class="sffv_no-results">No matching viewing angles.</div>'
        //    }
        //},
        collapsible: true,
    })
);

search.addWidget(
    instantsearch.widgets.refinementList({
        container: '#verticalViewingAngleRefinementList',
        attributeName: 'VerticalViewingAngle',
        sortBy: ['isRefined', 'count:desc', 'name:asc'],
        limit: 4,
        showMore: true,
        operator: 'or',
        templates: {
            item: facetTemplateRefinementListCheckBox,
            header: '<button class="btn btn-block btn-primary rounded-0">Vertical viewing angle</button>'
        },
        //searchForFacetValues: {
        //    isAlwaysActive: true,
        //    placeholder: 'Search for viewing angle',
        //    templates: {
        //        noResults: '<div class="sffv_no-results">No matching viewing angles.</div>'
        //    }
        //},
        collapsible: true,
    })
);

//brightness
search.addWidget(
    instantsearch.widgets.refinementList({
        container: '#brightnessRefinementList',
        attributeName: 'Brightness',
        sortBy: ['isRefined', 'count:desc', 'name:asc'],
        limit: 4,
        showMore: true,
        operator: 'or',
        templates: {
            item: facetTemplateRefinementListCheckBox,
            header: '<button class="btn btn-block btn-primary rounded-0">Brightness</button>'
        },
        //searchForFacetValues: {
        //    isAlwaysActive: true,
        //    placeholder: 'Search for brightness',
        //    templates: {
        //        noResults: '<div class="sffv_no-results">No matching brightness.</div>'
        //    }
        //},
        collapsible: true,
    })
);

//response time filter
search.addWidget(
    instantsearch.widgets.refinementList({
        container: '#responseTimeRefinementList',
        attributeName: 'MinimumResponceTime',
        sortBy: ['isRefined', 'count:desc', 'name:asc'],
        limit: 4,
        showMore: true,
        operator: 'or',
        templates: {
            item: facetTemplateRefinementListCheckBox,
            header: '<button class="btn btn-block btn-primary rounded-0">Response time</button>'
        },
        //searchForFacetValues: {
        //    isAlwaysActive: true,
        //    placeholder: 'Search for response time',
        //    templates: {
        //        noResults: '<div class="sffv_no-results">No matching response times.</div>'
        //    }
        //},
        collapsible: true,
    })
);

//refresh rate refinement list
search.addWidget(
    instantsearch.widgets.refinementList({
        container: '#refreshRateRefinementList',
        attributeName: 'VerticalFrequency',
        sortBy: ['isRefined', 'count:desc', 'name:asc'],
        limit: 4,
        showMore: true,
        operator: 'or',
        templates: {
            item: facetTemplateRefinementListCheckBox,
            header: '<button class="btn btn-block btn-primary rounded-0">Refresh rate</button>'
        },
        //searchForFacetValues: {
        //    isAlwaysActive: true,
        //    placeholder: 'Search for refresh rate',
        //    templates: {
        //        noResults: '<div class="sffv_no-results">No matching rates.</div>'
        //    }
        //},
        collapsible: true,
    })
);


//static contrast ratio slider
search.addWidget(
    instantsearch.widgets.refinementList({
        container: '#staticContrastRefinementList',
        attributeName: 'StaticContrast',
        sortBy: ['isRefined', 'count:desc', 'name:asc'],
        limit: 4,
        showMore: true,
        operator: 'or',
        templates: {
            item: `
    <a href="javascript:void(0);" class="">
        <input type="checkbox" class="{{cssClasses.checkbox}} m-2" value="{{label}}" {{#isRefined}}checked{{/isRefined}} />{{label}}
        <span class="facet-count">: 1 ({{count}})</span>
    </a>`,
            header: '<button class="btn btn-block btn-primary rounded-0">Static contrast</button>'
        },
        //searchForFacetValues: {
        //    isAlwaysActive: true,
        //    placeholder: 'Search for static contrast ratios',
        //    templates: {
        //        noResults: '<div class="sffv_no-results">No matching ratios.</div>'
        //    }
        //},
        collapsible: true,
    })
);

//dynamic contrast ratio slider
search.addWidget(
    instantsearch.widgets.refinementList({
        container: '#dynamicContrastRefinementList',
        attributeName: 'DynamicContrast',
        sortBy: ['isRefined', 'count:desc', 'name:asc'],
        limit: 4,
        showMore: true,
        operator: 'or',
        templates: {
            item: `
    <a href="javascript:void(0);" class="">
        <input type="checkbox" class="{{cssClasses.checkbox}} m-2" value="{{label}}" {{#isRefined}}checked{{/isRefined}} />{{label}}
        <span class="facet-count">: 1 ({{count}})</span>
    </a>`,
            header: '<button class="btn btn-block btn-primary rounded-0">Dynimic contrast</button>'
        },
        //searchForFacetValues: {
        //    isAlwaysActive: true,
        //    placeholder: 'Search for dynamic contrast ratios',
        //    templates: {
        //        noResults: '<div class="sffv_no-results">No matching ratios.</div>'
        //    }
        //},
        collapsible: true,
    })
);

//bool example
//search.addWidget(
//    instantsearch.widgets.toggle({
//        container: '#tiltToggle',
//        attributeName: 'Tilt',
//        label: 'Tilt',
//        templates: {
//            item: facetTemplateCheckbox,
//            footer: '<hr/>'
//        },
//        values: {
//            on: true
//        }
//    })
//);

search.start();